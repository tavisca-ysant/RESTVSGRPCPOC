using Grpc.Core;
using GrpcServer.Data;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class MeteoriteLandingsService : MeteoriteLandings.MeteoriteLandingsBase
    {
        public override Task<Version> GetVersion(EmptyRequest request, ServerCallContext context)
        {
            return Task.FromResult(new Version
            {
                ApiVersion = "API Version 1.0"
            });
        }
        public override async Task GetLargePayload(EmptyRequest request, IServerStreamWriter<MeteoriteLanding> responseStream, ServerCallContext context)
        {
            foreach(var meteorite in MeteoriteLandingData.GrpcMeteoriteLandings)
            {
                await responseStream.WriteAsync(meteorite);
            }
        }
        public override Task<MeteoriteLandingsList> GetLargePayloadAsList(EmptyRequest request, ServerCallContext context)
        {
            return Task.FromResult(MeteoriteLandingData.GrpcMeteoriteLandingsList);
        }
        public override Task<StatusResponse> PostLargePayload(MeteoriteLandingsList request, ServerCallContext context)
        {
            return Task.FromResult(new StatusResponse { Status = "Success" });
        }
    }
}
