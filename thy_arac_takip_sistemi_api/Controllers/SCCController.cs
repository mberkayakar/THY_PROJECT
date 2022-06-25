using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SCCController : ControllerBase
    {

        private readonly ILogger<SCCController> _logger;
        private readonly ISCC scc;

        public SCCController(ISCC _scc, ILogger<SCCController> logger)
        {
            _logger = logger;
            scc = _scc;
        }
        //TODO Comment
        [HttpGet]
        public IActionResult GetAllSCC()
        {
            _logger.LogInformation("Getting ALL SCC starting.");

            IQueryable<SCC> model = scc.GetAllSCC;
            if (model == null)
            {
                _logger.LogError("Getting ALL SCC model is null!");

                return NotFound();
            }
            _logger.LogInformation("Getting ALL SCC finished.");

            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateSCC([FromBody] SCC _scc)
        {
            _logger.LogInformation("Create SCC. Scc code : "+ _scc.code);
            if (_scc == null)
            {
                _logger.LogError("Create SCC failed. Scc code : "+ _scc.code);
                return NotFound();
            }

            POJO model = scc.CreateSCC(scc: _scc);
            if (model.Flag == true)
            {
                _logger.LogInformation("Create SCC OK. SCC code :"+ _scc.code);
                return Ok(model);

            }
            else
            {
                _logger.LogError("Create SCC Failed pts OK.SCC code :"+ _scc.code, "exception : "+ model.Message);

                return NotFound(model);
            }

        }
        [HttpPut]
         public IActionResult ModifySCC([FromBody] SCC _scc)
        {
            _logger.LogInformation("Modify SCC. Scc code : "+ _scc.code);
            if (_scc == null)
            {
                _logger.LogError("Modify SCC failed. Scc code : "+ _scc.code);
                return NotFound();
            }

            POJO model = scc.ModifySCC(scc: _scc);
            if (model.Flag == true)
            {
                _logger.LogInformation("Modify SCC OK. SCC code :"+ _scc.code);
                return Ok(model);

            }
            else
            {
                _logger.LogError("Modify SCC Failed pts OK.SCC code :"+ _scc.code, "exception : "+ model.Message);

                return NotFound(model);
            }

        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteSCC(int? id)
        {
            _logger.LogInformation("Delete SCC From Id Starting");

            if (id==null|| id == 0)
            {
                _logger.LogError("Delete SCC From Id Failed Id is Null or Equal 0");

                return NotFound();
            }
            POJO model = scc.DeleteSCC(id.Value);
            if (model.Flag == false)
            {
                _logger.LogError("Delete SCC From Id Failed id is "+id.Value+" Exception:"+ model.Message);

                return NotFound(model);
            }
            _logger.LogInformation("Delete SCC From Id Success id is: "+id.Value);
            return Ok(model);
        }
    
    }
}
