using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;
using thy_arac_takip_sistemi_api.Models.ControllerObjects;

namespace thy_arac_takip_sistemi_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PTSLogController : ControllerBase
    {

        private readonly ILogger<PTSLogController> _logger;
        private readonly IPTSLog pts;
        public PTSLogController(IPTSLog _pts, ILogger<PTSLogController> logger)
        {
            pts = _pts;
            _logger = logger;
        }
        //TODO log

        [HttpGet]
        public IActionResult GetPlateEntries()
        {
            IQueryable<PTS> plateEntries = pts.GetPlateEntries;

            if (plateEntries!= null)
            {
                return Ok(plateEntries);
            }
            return NotFound();

        }
        [HttpDelete]
        public IActionResult DeletePlateEntry(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            POJO model = pts.DeletePlateEntry(id:id.Value);
            if(model.Flag==true)
            {
                return Ok(model);
            }

            return NotFound(model);
        }

        [HttpPut]
        public IActionResult UpdatePlate([FromBody]PTS _pts)
        {
            if(_pts==null || _pts.plate==null)
            {
                return BadRequest();
            }
            PTS mypts = pts.UpdatePlate(_pts);
            if (mypts != null)
            {
                return Ok(mypts);
            }
            return NotFound();
        }
        [HttpPost]
        [Route("delete")]
        public IActionResult DeletePlateEntries([FromBody] ListNumber idList)
        {
            if(idList==null || idList.idList == null)
            {
                return BadRequest();
            }
            POJO model = pts.DeletePlateEntries(idList: idList.idList);
            if (model.Flag == true)
            {
                return Ok(model);
            }

            return NotFound(model);
        }
    }
}
