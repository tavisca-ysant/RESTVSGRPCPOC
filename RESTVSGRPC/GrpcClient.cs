using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RESTVSGRPC
{
    public class GrpcClient
    {
        private  GrpcChannel _channel;
        private  MeteoriteLandings.MeteoriteLandingsClient _client;

        public GrpcClient()
        {
            _channel = GrpcChannel.ForAddress("https://localhost:5001/meteorite");
            _client =  new MeteoriteLandings.MeteoriteLandingsClient(_channel);
        }

        public async Task<string> GetSmallPayloadAsync()
        {
            var response = await _client.GetVersionAsync(new EmptyRequest());
            return response.ApiVersion;
        }
        public async Task<List<MeteoriteLanding>> StreamLargePayloadAsync()
        {
            var meteorites = new List<MeteoriteLanding>();
            using var response = _client.GetLargePayload(new EmptyRequest());
            try
            {
                await foreach (var data in response.ResponseStream.ReadAllAsync())
                {
                    meteorites.Add(data);
                }
            }
            catch (RpcException ex) { }
            return meteorites;
        }

        public async Task<MeteoriteLandingsList> LargePayloadAsListAsync()
        {
            return await _client.GetLargePayloadAsListAsync(new EmptyRequest());
        }
        public async Task<string> PostLargePayloadAsync(MeteoriteLandingsList meteoriteLandingsList)
        {
            var response = await _client.PostLargePayloadAsync(meteoriteLandingsList);
            return response.Status;
        }
    }
}
