using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;
using thy_arac_takip_sistemi_api.Repo;

namespace thy_arac_takip_sistemi_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly ILogger<LogsController> _logger;
        private readonly ILogs logs;

        public LogsController(ILogs _logs, ILogger<LogsController> logger)
        {
            _logger = logger;
            logs = _logs;
        }

        //TODO log

        [HttpGet]
        public IActionResult GetLogs()
        {
            IQueryable<NLogItem> nLogs = logs.GetLogs;

            if (nLogs != null)
            {
                return Ok(nLogs);
            }
            return NotFound();

        }
    }
}
