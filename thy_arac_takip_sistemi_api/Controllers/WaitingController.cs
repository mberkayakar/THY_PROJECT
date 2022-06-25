using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;
using thy_arac_takip_sistemi_api.Models.ControllerObjects;
using thy_arac_takip_sistemi_api.Repo;

namespace thy_arac_takip_sistemi_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WaitingController : ControllerBase
    {
        private readonly ILogger<WaitingController> _logger;
        private readonly IWaiting waiting;

        public WaitingController(IWaiting _waiting, ILogger<WaitingController> logger)
        {
            _logger = logger;
            waiting = _waiting;
        }

        //TODO log

        [HttpGet]
        public IActionResult GetWaitings()
        {
            IQueryable<WaitingQueue> waitingQueues = waiting.GetWaitingQuery;

            if (waitingQueues != null)
            {
                return Ok(waitingQueues);
            }
            return NotFound();

        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteWaiting(int? id)
        {
            _logger.LogInformation("Delete Waiting From Id Starting");

            if (id == null || id == 0)
            {
                _logger.LogError("Delete Waiting From Id Failed Id is Null or Equal 0");

                return NotFound();
            }
            POJO model = waiting.DeleteWaiting(id.Value);
            if (model.Flag == false)
            {
                _logger.LogError("Delete Waiting From Id Failed id is " + id.Value + " Exception:" + model.Message);

                return NotFound(model);
            }
            _logger.LogInformation("Delete Waiting From Id Success id is: " + id.Value);
            return Ok(model);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult DeletePlateEntries([FromBody] ListNumber idList)
        {
            if (idList == null || idList.idList == null)
            {
                return BadRequest();
            }
            POJO model = waiting.DeleteWaitings(idList: idList.idList);
            if (model.Flag == true)
            {
                return Ok(model);
            }

            return NotFound(model);
        }
    }
}
