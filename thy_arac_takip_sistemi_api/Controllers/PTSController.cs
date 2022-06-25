using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;
namespace thy_arac_takip_sistemi_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PTSController : ControllerBase
    {
        private readonly ILogger<ReaderController> _logger;
        private readonly IPTS pts;

        public PTSController(IPTS _pts, ILogger<ReaderController> logger)
        {
            _logger = logger;
            pts = _pts;
        }
        //TODO comment
        [HttpPost]
        public IActionResult CheckReservation([FromForm] PTS _pts, IFormFile file)
        {
            _logger.LogInformation("Check reservation with pts. Plate : " + _pts.plate);
            if (_pts == null)
            {
                _logger.LogError("Check reservation failed.Plate : " + _pts.plate);

                return NotFound();
            }

            POJO model = pts.CheckReservationPTS(pts: _pts, file: file);
            if (model.Flag == true)
            {
                _logger.LogInformation("Check reservation with pts OK.Plate : " + _pts.plate + "State : " + model.Message);

            }
            else
            {
                _logger.LogInformation("Check reservation with pts OK but reservation not found.Plate : " + _pts.plate + "State : " + model.Message);

            }

            return Ok(model);
        }


    }
}
