using System;

using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using thy_arac_takip_sistemi_api.Models.ControllerObjects;
using thy_arac_takip_sistemi_api.Repo;

namespace thy_arac_takip_sistemi_api.Controllers
{
    [ApiController]

    [Route("[controller]")]

    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private MyDbContext dbContext;

        public TestController(ILogger<TestController> logger,MyDbContext _db)
        {
            _logger = logger;
            dbContext = _db;
        }
        [HttpGet]
        public IActionResult GetTest()
        {
            return Ok("TK GO Backend Test Page");
        }
        [HttpGet]
        [Route("image")]
        public IActionResult Get(string fileName)
        {
            var image = System.IO.File.OpenRead("./thy_images/"+fileName);
            return File(image, "image/jpeg");
        }

        [HttpGet]
        [Route("eventtest")]
        public IActionResult GetTestEvent()
        {
            dbContext.EventLogs.Add(
                new EventLog
            {
                eventLogType = EventLogTypes.CarArrivedPTS
            ,
                message = "3412",
            });
            dbContext.SaveChanges();
            return Ok();
        }
        /*   [HttpPost]
          public IActionResult ImageGet([FromForm] IFormFile file)
          {
              try
              {
                  // 1. get the file form the request
                  var postedFile = file;
                  // 2. set the file uploaded folder
                  var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "images");
                  // 3. check for the file length, if it is more than 0 the save it
                  if (postedFile.Length > 0)
                  {
                      // 3a. read the file name of the received file
                      var fileName = ContentDispositionHeaderValue.Parse(postedFile.ContentDisposition)
                          .FileName.Trim('"');
                      // 3b. save the file on Path
                      var finalPath = Path.Combine(uploadFolder, fileName);
                      using (var fileStream = new FileStream(finalPath, FileMode.Create))
                      {
                          postedFile.CopyTo(fileStream);
                      }
                      return Ok($"File is uploaded Successfully");
                  }
                  else
                  {
                      return BadRequest("The File is not received.");
                  }


              }
              catch (Exception ex)
              {
                  return StatusCode(500, $"Some Error Occcured while uploading File {ex.Message}");
              }
              //fileModel save to db

          } */
    }

}

