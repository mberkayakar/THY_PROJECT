using System;

using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using thy_arac_takip_sistemi_api.Models.ControllerObjects;

namespace thy_arac_takip_sistemi_api.Controllers
{
    [ApiController]

    [Route("[controller]")]

    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;

        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }
       
        [HttpGet]
//TODO log
public IActionResult Get(string fileName)
{
    var image = System.IO.File.OpenRead("./thy_images/"+fileName);
    return File(image, "image/jpeg");
}
       
    }

}

