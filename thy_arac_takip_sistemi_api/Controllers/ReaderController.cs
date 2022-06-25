using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReaderController : ControllerBase
    {
        private readonly ILogger<ReaderController> _logger;
        private readonly IReaderModule reader;

        public ReaderController(IReaderModule _reader, ILogger<ReaderController> logger)
        {
            _logger = logger;
            reader = _reader;
        }
            //TODO comment and log

        [HttpGet]
        public IActionResult GetReaderModulesAll()
        {
            _logger.Log(logLevel: LogLevel.Information, "Get All Reader Modules Trying");
            IQueryable<ReaderModule> model = reader.GetReaderModulesAll;

            if (model == null)
            {
                _logger.Log(logLevel: LogLevel.Warning, "Get All Reader Modules Trying Failed Model Is Null Returning Not Found");

                return NotFound();
            }
            _logger.Log(logLevel: LogLevel.Information, "Get All Reader Modules OK! RETURNING MODEL");

            return Ok(model);
        }


        [HttpPost]
        public IActionResult CreateModule([FromBody] ReaderModule readerModule)
        {
            _logger.LogInformation("Create Reader Module Starting");
            if (readerModule == null)
            {
                _logger.LogError("Create Reader Module - readerModule is null Exiting Not found");

                return NotFound("Reader Null");
            }

            POJO model = reader.CreateReaderModule(readerModule: readerModule);
            _logger.LogInformation("Create Reader Module Finished Returning OK");

            return Ok(model);
        }
        [HttpGet]
        [Route("updatestate")]
        public IActionResult UpdateDoorStates(int? readerId, string doorData)
        {
            _logger.LogInformation("Update Door States Starting");


            if (readerId == null || doorData == null)
            {
                _logger.LogError("Update Door States ReaderId or Door Data null");

                return NotFound();
            }
            POJO model = reader.UpdateDoorStates(readerId.Value, doorData);

            if (model.Flag == false)
            {
                _logger.LogError("Update Door States ReaderId or Door Data NOT null But Flag is False Exception : " + model.Message + "  : readerID:" + readerId.Value + " --- data: " + doorData);

                return NotFound(model);
            }
            _logger.LogInformation("Update Door States Finished Returning OK readerModuleID:" + readerId.Value.ToString() + " door data " + doorData);

            return Ok(model);

        }

        [HttpGet]
        [Route("orders")]
        public IActionResult GetOrders(int? readerId)
        {
            _logger.LogInformation("Send Door Orders Starting");


            if (readerId == null)
            {
                _logger.LogError("Send Door Orders ReaderId");

                return NotFound();
            }
            POJO model = reader.GetOrders(readerId.Value);

            if (model.Flag == false)
            {
                _logger.LogError("Send Door States ReaderId or Door Data NOT null But Flag is False Exception : " + model.Message + "  : readerID:" + readerId.Value);

                return NotFound(model);
            }
            _logger.LogInformation("Send Door States Finished Returning OK readerModuleID:" + readerId.Value.ToString() + "  sended message " + model.Message);

            return Ok(model);

        }
        ///DELETE
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteReaderModule(int? id)
        {
            _logger.LogInformation("Delete DeleteReaderModule Starting");

            if (id == null || id == 0)
            {
                _logger.LogError("Delete DeleteReaderModule Failed id null or equal 0");

                return NotFound();
            }
            POJO model = reader.DeleteReaderModule(id.Value)
                ;
            if (model.Flag == false)
            {
                _logger.LogError("Delete DeleteReaderModule Failed  Exception: " + model.Message);

                return NotFound(model);
            }
            _logger.LogInformation("Delete DeleteReaderModule Success");

            return Ok(model);
        }


    }
}
