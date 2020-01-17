using ModelLibrary.REST;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RESTVSGRPC
{
    public class RESTClient
    {
        private static readonly HttpClient client = new HttpClient();
        public async Task<string> GetSmallPayloadAsync()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.GetStringAsync("https://localhost:50001/meteoritelandings");
        }

        public async Task<List<MeteoriteLanding>> GetLargePayloadAsync()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string meteoriteLandingsString = await client.GetStringAsync("https://localhost:50001/meteoritelandings/largepayload");

            return JsonConvert.DeserializeObject<List<MeteoriteLanding>>(meteoriteLandingsString);
        }

        public async Task<string> PostLargePayloadAsync(List<MeteoriteLanding> meteoriteLandings)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var response = await client.PostAsync("https://localhost:50001/meteoritelandings/largepayload", meteoriteLandings, new JsonMediaTypeFormatter());

            return await response.Content.ReadAsStringAsync();
        }
    }
}
