using ModelLibrary.REST;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Data;
using System.Collections.Generic;

namespace RestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeteoriteLandingsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "API Version 1.0";
        }
        [HttpGet]
        [Route("LargePayload")]
        public ActionResult<List<MeteoriteLanding>> GetLargePayload()
        {
            return MeteoriteLandingData.RestMeteoriteLandings;
        }

        [HttpPost]
        [Route("LargePayload")]
        public string PostLargePayload([FromBody] List<MeteoriteLanding> meteoriteLandings)
        {
            return "SUCCESS";
        }
    }
}
