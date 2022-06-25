using System.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;
using thy_arac_takip_sistemi_api.Models.ControllerObjects;

namespace thy_arac_takip_sistemi_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    //TODO logger
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly IReservation reservation;

        public ReservationController(IReservation _reservation, ILogger<ReservationController> logger)
        {
            _logger = logger;
            reservation = _reservation;
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult GetReservations()
        {

            _logger.Log(logLevel: LogLevel.Information, "Get All Reservations Trying");

            List<Reservation> model = reservation.GetReservations();
            if (model == null)
            {
                _logger.Log(logLevel: LogLevel.Warning, "Get All Reservations Trying Failed Model Is Null Returning Not Found");

                return NotFound();
            }
            _logger.Log(logLevel: LogLevel.Information, "Get All Reservations OK! RETURNING MODEL");

            return Ok(model);

        }
        [HttpGet]
        public IActionResult GetReservationsOnlyNotDoorAssigned()
        {

            _logger.Log(logLevel: LogLevel.Information, "GetReservationsOnlyNotDoorAssigned Trying");

            IQueryable<Reservation> model = reservation.GetReservationsNotDoorAssigned;
            if (model == null)
            {
                _logger.Log(logLevel: LogLevel.Warning, "GetReservationsOnlyNotDoorAssigned Trying Failed Model Is Null Returning Not Found");

                return NotFound();
            }
            _logger.Log(logLevel: LogLevel.Information, "GetReservationsOnlyNotDoorAssigned OK! RETURNING MODEL");

            return Ok(model);

        }
        [HttpGet]
        [Route("active/{agentId}")]
        public IActionResult GetReservationsFromAgentId(long? agentId)
        {
            _logger.Log(logLevel: LogLevel.Information, "Get Agent Reservations With AgentId Trying");

            if (agentId == null || agentId == 0)
            {
                _logger.Log(logLevel: LogLevel.Warning, "Get Agent Reservations  Trying Failed AgentId Is Null or equal= 0 Returning Not Found");

                return NotFound();
            }
            else
            {
                _logger.Log(logLevel: LogLevel.Information, "Get Agent Reservations With AgentId Trying agentID =  " + agentId.ToString());

                List<Reservation> model = reservation.GetActiveReservationsWithAgentId(agentId: agentId.Value);
                if (model == null)
                {
                    _logger.Log(logLevel: LogLevel.Warning, "Get Agent Reservations  Trying Failed Model Is Null Returning Not Found");

                    return NotFound();
                }
                _logger.Log(logLevel: LogLevel.Information, "Get Agent Reservations OK agentId= " + agentId.ToString());

                return Ok(model);
            }
        }
        [HttpGet]
        [Route("old/{agentId}")]
        public IActionResult GetAllReservationsFinished(long? agentId)
        {
            _logger.Log(logLevel: LogLevel.Information, "Get Old Agent Reservations With AgentId Trying");

            if (agentId == null || agentId == 0)
            {
                _logger.Log(logLevel: LogLevel.Warning, "Get Old  Agent Reservations  Trying Failed AgentId Is Null or equal= 0 Returning Not Found");

                return NotFound();
            }
            else
            {
                _logger.Log(logLevel: LogLevel.Information, "Get  Old Agent Reservations With AgentId Trying agentID =  " + agentId.ToString());

                List<Reservation> model = reservation.GetAllReservationsFinishedWithAgentId(agentId: agentId.Value);
                if (model == null)
                {
                    _logger.Log(logLevel: LogLevel.Warning, "Get Old  Agent Reservations  Trying Failed Model Is Null Returning Not Found");

                    return NotFound();
                }
                _logger.Log(logLevel: LogLevel.Information, "Get  Old Agent Reservations OK agentId= " + agentId.ToString());

                return Ok(model);
            }
        }
        [HttpGet]
        [Route("passive/{agentId}")]
        public IActionResult GetPassiveReservations(long? agentId)
        {
            _logger.Log(logLevel: LogLevel.Information, "Get Passive Agent Reservations With AgentId Trying");

            if (agentId == null || agentId == 0)
            {
                _logger.Log(logLevel: LogLevel.Warning, "Get Passive  Agent Reservations  Trying Failed AgentId Is Null or equal= 0 Returning Not Found");

                return NotFound();
            }
            else
            {
                _logger.Log(logLevel: LogLevel.Information, "Get  Passive Agent Reservations With AgentId Trying agentID =  " + agentId.ToString());

                List<Reservation> model = reservation.GetPassiveReservationsWithAgentId(agentId: agentId.Value);
                if (model == null)
                {
                    _logger.Log(logLevel: LogLevel.Warning, "Get Passive  Agent Reservations  Trying Failed Model Is Null Returning Not Found");

                    return NotFound();
                }
                _logger.Log(logLevel: LogLevel.Information, "Get  Passive Agent Reservations OK agentId= " + agentId.ToString());

                return Ok(model);
            }
        }
        [HttpGet]
        [Route("plate")]
        public IActionResult GetReservationsFromPlate(string plate)
        {
            if (plate == null || plate == "")
            {
                return NotFound();
            }
            else
            {
                Reservation model = reservation.GetReservationFromPlate(plate: plate);
                if (model == null)
                {
                    return NotFound();
                }

                return Ok(model);
            }
        }
        //Get reservation from plate but for pts
        //? This function will return different string  
        [HttpGet]
        [Route("pts")]
        public IActionResult GetReservationWithPlateForPTS(string plate)
        {
            if (plate == null || plate == "")
            {
                return NotFound();
            }
            else
            {
                Reservation model = reservation.GetReservationFromPlate(plate: plate);
                //TODO configure returned object
                if (model == null)
                {
                    return NotFound();
                }

                return Ok(model);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetReservationFromId(int? id)
        {
            _logger.LogInformation("Get Reservation From Id Starting");
            if (id == null || id == 0)
            {
                _logger.LogError("Get Reservation From Id Failed id null or equal 0");

                return NotFound();
            }
            else
            {
                Reservation model = reservation.GetReservationFromId(id: id.Value);
                if (model == null)
                {
                    _logger.LogError("Get Reservation From Id Failed");

                    return NotFound();
                }
                _logger.LogError("Get Reservation From Id Success");

                return Ok(model);
            }
        }
        //Get reservations between two arrive date,
        [HttpGet]
        [Route("arrivedate")]
        public IActionResult GetReservationsBetweenTwoArriveDate([FromBody] ArriveDate arrive)
        {
            _logger.LogInformation("Get Reservations Between Two Arrive Date Starting");


            if (arrive == null || arrive.arriveDateFinish == null || arrive.arriveDateStart == null)
            {

                _logger.LogError("Get Reservations Between Two Arrive Date Failed Null variables");

                return NotFound();
            }
            else
            {
                IQueryable<Reservation> model = reservation.GetReservationsBetweenTwoArriveDate(start: arrive.arriveDateStart, finish: arrive.arriveDateFinish);
                if (model == null)
                {
                    _logger.LogError("Get Reservations Between Two Arrive Date Failed ");

                    return NotFound();
                }
                _logger.LogInformation("Get Reservations Between Two Arrive Date Success");

                return Ok(model);
            }
        }
        ///
        /// POST
        [HttpPost]
        public IActionResult PostReservation([FromBody] Reservation res)
        {
            _logger.LogInformation("Post Reservation From Body Starting");

            if (res == null)
            {
                _logger.LogError("Post Reservation From Body Failed Body is Null ?! ");

                return NotFound();
            }
            else
            {
                Reservation _res = reservation.GetReservationFromPlate(res.carPlate);
                if (_res != null && res.agentId == _res.agentId && _res.isUnload == false && _res.isActive == true)
                {
                    _logger.LogError("Post Reservation Failed Reservation exist");

                    return Conflict("Aynı plakaya aktif bir rezervasyon bulunmaktadır.");
                }
                POJO model = reservation.CreateReservation(res);
                if (model.Flag == false)
                {
                    _logger.LogError("Post Reservation From Body Failed Exception :  " + model.Message);

                    return NotFound(model);
                }
                _logger.LogInformation("Post Reservation From Body Success");

                return Ok(model);
            }
        }
        //MODIFY PUT
        [HttpPut]
        public IActionResult PutReservation([FromBody] Reservation res)
        {
            _logger.LogInformation("Modify Reservation From Body Starting");


            if (res == null)
            {
                _logger.LogError("Modify Reservation From Body Failed Body is Null ?! ");

                return NotFound();
            }
            else
            {
                POJO model = reservation.ModifyReservation(res);
                if (model.Flag == false)
                {
                    _logger.LogError("Modify Reservation From Body Failed Exception :  " + model.Message);

                    return NotFound(model);
                }
                _logger.LogInformation("Modidy Reservation From Body Success");

                return Ok(model);
            }
        }
        ///DELETE
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteReservation(int? id)
        {
            _logger.LogInformation("Delete Reservation From Id Starting");

            if (id == null || id == 0)
            {
                _logger.LogError("Delete Reservation From Id Failed Id is Null or Equal 0");

                return NotFound();
            }
            POJO model = reservation.DeleteReservation(id.Value)
                ;
            if (model.Flag == false)
            {
                _logger.LogError("Delete Reservation From Id Failed Exception:" + model.Message);

                return NotFound(model);
            }
            _logger.LogInformation("Delete Reservation From Id Success");
            return Ok(model);
        }
        [HttpPost]
        [Route("delete")]
        public IActionResult DeleteReservations([FromBody] ListNumber idList)
        {
            if (idList == null || idList.idList == null)
            {
                return BadRequest();
            }
            POJO model = reservation.DeleteReservations(idList: idList.idList);
            if (model.Flag == true)
            {
                return Ok(model);
            }

            return NotFound(model);
        }
    }
}
