using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using Microsoft.AspNetCore.Mvc;

namespace GrpcClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static GrpcChannel _channel = GrpcChannel.ForAddress("https://localhost:5001/meteorite");
        private static MeteoriteLandings.MeteoriteLandingsClient _client = new MeteoriteLandings.MeteoriteLandingsClient(_channel);
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            var reply = await _client.GetVersionAsync(new EmptyRequest());
            return Ok(reply.ApiVersion);
            
        }

        [HttpGet]
        [Route("LargePayloadAsList")]
        public async Task<MeteoriteLandingsList> GetLargePayloadAsList()
        {
            
            return await _client.GetLargePayloadAsListAsync(new EmptyRequest());
        }
        [HttpGet]
        [Route("LargePayload")]
        public async Task<List<MeteoriteLanding>> GetLargePayload()
        {
            var meteorites = new List<MeteoriteLanding>();
            using var response = _client.GetLargePayload(new EmptyRequest());
            try
            {
                await foreach(var data in response.ResponseStream.ReadAllAsync())
                {
                    meteorites.Add(data);
                }
            }
            catch (RpcException ex) { }
            return meteorites;
        }
    }
}
