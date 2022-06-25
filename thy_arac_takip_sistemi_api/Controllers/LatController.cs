using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LatController : ControllerBase
    {

        private readonly ILogger<LatController> _logger;
        private readonly ILat lat;
        public LatController(ILat _plat, ILogger<LatController> logger)
        {
            lat = _plat;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetLAT(string sccText)
        {
            //TODO logging
            if (sccText == null)
            {
                return NotFound();
            }

            LatSCC model = lat.GetLatSCC(sccText);
            if (model.lat != null)
            {
                return Ok(model);
            }
            return NotFound();

        }
        [HttpPost]
        public IActionResult PostLAT([FromBody] LatSCC latScc)
        {
            if (latScc == null)
            {
                return NotFound();
            }
            POJO model = lat.PostLatSCC(latScc);
            if (model.Flag == true)
            {
                return Ok(model);
            }
            return NotFound(model);
        }

    }
}
