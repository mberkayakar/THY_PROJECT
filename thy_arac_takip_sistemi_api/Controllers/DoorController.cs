using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DoorController : ControllerBase
    {

        private readonly ILogger<DoorController> _logger;
        private readonly IDoor door;
        private readonly ISCC scc;

        public DoorController(IDoor _door, ISCC _scc, ILogger<DoorController> logger)
        {
            _logger = logger;
            door = _door;
            scc = _scc;
        }
        //Get all reader modules
        [HttpGet]
        public IActionResult GetReaderModulesAll()
        {
            _logger.Log(logLevel: LogLevel.Information, "Get All Doors  Trying");
            IQueryable<Door> model = door.GetDoorAll;

            if (model == null)
            {

                _logger.Log(logLevel: LogLevel.Warning, "Get All Doors Trying Failed Model Is Null Returning Not Found");

                return NotFound();
            }
            _logger.Log(logLevel: LogLevel.Information, "Get All Doors OK! RETURNING MODEL");

            return Ok(model);
        }



        //Match door and scc door id and scc id
        [HttpPost]
        [Route("match")]
        public IActionResult MatchDoorAndScc([FromBody] DoorAndSCC doorAndSCC)
        {
            if (doorAndSCC == null)
            {
                return NotFound();
            }

            POJO model = door.MatchDoorAndSCC(doorAndScc: doorAndSCC);
            if (model.Flag == true)
            {
                return Ok(model);
            }
            return NotFound(model);
        }
        [HttpPost]
        [Route("matchlist")]
        public IActionResult MatchDoorAndSccList([FromBody] DoorAndSCCList doorAndSCC)
        {
            if (doorAndSCC == null)
            {
                return NotFound();
            }

            POJO model = door.MatchDoorAndSCCList(doorAndScc: doorAndSCC);
            if (model.Flag == true)
            {
                return Ok(model);
            }
            return NotFound(model);
        }


        [HttpGet]
        [Route("doorscc")]
        public IActionResult CheckDoor(string sccText)
        {
            _logger.Log(logLevel: LogLevel.Information, "GetDoorAvaliableWithSCC Trying");


            if (scc == null)
            {
                _logger.Log(logLevel: LogLevel.Error, "GetDoorAvaliableWithSCC SCC is null");
                return NotFound();
            }
            SCC sc = scc.GetSCCWithText(sccText);
            if (sc == null)
            {
                _logger.Log(logLevel: LogLevel.Error, "GetDoorAvaliableWithSCC SCC is not null But has no scc like this");
                return NotFound();
            }
            Door _door = door.GetDoorAvaliableWithSCC(sc);
            if (_door == null)
            {
                _logger.Log(logLevel: LogLevel.Error, "GetDoorAvaliableWithSCC SCC is not null Door not found");

                return NotFound();

            }
            _logger.Log(logLevel: LogLevel.Information, "GetDoorAvaliableWithSCC OK door no:" + _door.doorNumber);

            return Ok(_door);
        }
        [HttpPut]
        public IActionResult ModifySCC([FromBody] Door _door)
        {
            _logger.LogInformation("Modify Door. Door  number : " + _door.doorNumber);
            if (_door == null)
            {
                _logger.LogError("Modify Door failed. Door number : " + _door.doorNumber);
                return NotFound();
            }

            POJO model = door.ModifyDoor(door: _door);
            if (model.Flag == true)
            {
                _logger.LogInformation("Modify Door OK. Door number :" + _door.doorNumber);
                return Ok(model);

            }
            else
            {
                _logger.LogError("Modify Door Failed pts OK. Door number:" + _door.doorNumber, "exception : " + model.Message);

                return NotFound(model);
            }

        }
        [HttpGet]
        [Route("remove-reservation")]
        public IActionResult RemoveReservation(int? doorId)
        {
            _logger.LogInformation("Removing Reservation Of Door.");
            if (doorId == null)
            {
                _logger.LogError("Removing Reservation  failed.Maybe doorId is null Door id : " + doorId.Value);
                return NotFound();
            }

            POJO model = door.RemoveReservation(doorId: doorId.Value);
            if (model.Flag == true)
            {
                _logger.LogInformation("Removing Reservation  OK. Door id :" + doorId.Value + "Message:" + model.Message);
                return Ok(model);

            }
            else
            {
                _logger.LogError("Removing Reservation  Failed pts OK. Door id:" + doorId.Value, "exception : " + model.Message);

                return NotFound(model);
            }
        }

        //TODO LOG 
        [HttpGet]
        [Route("manual-reservation")]
        public IActionResult MakeManualReservation(int? doorId, int? reservationId)
        {
            if (doorId == null || reservationId == null)
            {
                return NotFound("Null");
            }

            POJO model = door.MakeReservationManual(doorId.Value, reservationId.Value);
            if (model.Flag == false)
            {
                return NotFound(model);
            }
            return Ok(model);
        }

    }
}