using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;
using thy_arac_takip_sistemi_api.Models.ControllerObjects;

namespace thy_arac_takip_sistemi_api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ButtonEntryController : ControllerBase
    {
        private readonly IButtonEntry buttonEntry;
        private readonly ILogger<ButtonEntryController> _logger;

        public ButtonEntryController(IButtonEntry _button, ILogger<ButtonEntryController> logger)
        {
            buttonEntry = _button;
            _logger = logger;
        }
        //Button entries getted, file and button
        [HttpPost]
        public IActionResult CreateButtonEntry([FromForm] ButtonEntry button, IFormFile file)
        {
            _logger.LogInformation("Create Button Entry. Plate : " + button.plate);
            if (button == null&&file==null&&file.Length<=0)
            {
                _logger.LogError("Create Button Entry failed.  Plate : " + button.plate);
                return NotFound();
            }

            POJO model = buttonEntry.AddButtonEntry(button: button,file:file);
            if (model.Flag == true)
            {
                _logger.LogInformation("Create Button Entry. Plate :" + button.plate + " Message : "+ model.Message);
                return Ok(model);
            }
            else
            {
                _logger.LogError("Create Button Entry. OK.Plate :" + button.plate+"exception : " + model.Message);
                return NotFound(model);
            }
        }
        //TODO log

        [HttpGet]
        public IActionResult GetButtonEntries()
        {
            IQueryable<ButtonEntry> buttonEntries = buttonEntry.GetButtonEntries;

            if (buttonEntries != null)
            {
                return Ok(buttonEntries);
            }
            return NotFound();

        }
        [HttpDelete]
        public IActionResult DeleteButtonEntry(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            POJO model = buttonEntry.DeleteButtonEntry(id: id.Value);
            if (model.Flag == true)
            {
                return Ok(model);
            }

            return NotFound(model);
        }

     [HttpPut]
        public IActionResult UpdatePlate([FromBody] ButtonEntry btn)
        {
            if (btn == null || btn.plate == null)
            {
                return BadRequest();
            }
            ButtonEntry mybtn = buttonEntry.UpdateButtonEntry(btn);
            if (mybtn != null)
            {
                return Ok(mybtn);
            }
            return NotFound();
        }
        [HttpPost]
        [Route("delete")]
        public IActionResult DeleteButtonEntries([FromBody] ListNumber idList)
        {
            if (idList == null || idList.idList == null)
            {
                return BadRequest();
            }
            POJO model = buttonEntry.DeleteButtonEntries(idList: idList.idList);
            if (model.Flag == true)
            {
                return Ok(model);
            }

            return NotFound(model);
        }
    }
}
