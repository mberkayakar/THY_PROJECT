using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;
using thy_arac_takip_sistemi_api.Repo;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using thy_arac_takip_sistemi_api.Models.ControllerObjects;

namespace thy_arac_takip_sistemi_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AWBController : ControllerBase
    {
        private readonly ILogger<AWBController> _logger;
        private readonly IAWB _awbservice;


        public AWBController(ILogger<AWBController> logger, IAWB awbservice)
        {
            _logger = logger;
            _awbservice = awbservice;
        }

        //TODO log

        [HttpPost]
        async public Task<IActionResult> AWBSearch([FromBody] AWBItem awbItem)
        {
            HttpClient client = new HttpClient();

            string uri = "https://api.turkishairlines.com/test/cargo/shipment/history";

            client.DefaultRequestHeaders.Add(_awbservice.GetConfigValueWithKey(x => x.key == "apikey").key, _awbservice.GetConfigValueWithKey(x => x.key == "apikey").value);
            client.DefaultRequestHeaders.Add(_awbservice.GetConfigValueWithKey(x => x.key == "apisecret").key, _awbservice.GetConfigValueWithKey(x => x.key == "apisecret").value);

            var configs = new
            {
                requestHeader = new { clientTransactionId = _awbservice.GetConfigValueWithKey(x => x.key == "clientTransactionId").value},
                shipmentHistoryRequest = new
                {
                    trackingFilters = new object[]{
                        new{

                                shipmentPrefix = awbItem.shipmentPrefix,

                                masterDocumentNumber = awbItem.masterDocumentNumber,

                        }
                     },

                }
            };

            var jsonData = JsonConvert.SerializeObject(configs, Formatting.Indented);
            var res = await client.PostAsync(uri, new StringContent(jsonData, Encoding.UTF8, "application/json"));
            return Ok(res.Content.ReadAsStringAsync().Result);

        }
    }
}
