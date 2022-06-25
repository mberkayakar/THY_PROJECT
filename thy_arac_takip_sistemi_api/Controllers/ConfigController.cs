
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
    public class ConfigController : ControllerBase
    {
        private readonly ILogger<ConfigController> _logger;
        private readonly IConfig config;

        public ConfigController(IConfig _config, ILogger<ConfigController> logger)
        {
            _logger = logger;
            config = _config;
        }

        //TODO log

        [HttpGet]
        public IActionResult GetWaitings()
        {
            IQueryable<Config> configs = config.GetConfigs;

            if (configs != null)
            {
                return Ok(configs);
            }
            return NotFound();

        }
        [HttpPost]
        [Route("update")]
        public IActionResult UpdateConfig([FromBody] Config conf)
        {
            if (conf == null || conf.key == null || conf.value == null)
            {
                return BadRequest();
            }
            POJO model = config.UpdateConfig(config: conf);
            if (model != null)
            {
                return Ok(model);
            }
            return NotFound(model);
        }


    }
}
