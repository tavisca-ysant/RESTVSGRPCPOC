using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using GrpcServer;

namespace ModelLibrary.Data
{
    public class MeteoriteLandingData
    {
        static string meteoriteLandingsJson;
        public static string MeteoriteLandingsJson
        {
            get
            {
                if(meteoriteLandingsJson == null)
                {
                    
                    using (var reader = new StreamReader(@"C:\Users\ysant\source\repos\RESTGRPCPeer2Peer\ModelLibrary\Data\MeteoriteLandings.json"))
                    {
                        meteoriteLandingsJson = reader.ReadToEnd();
                    }
                }
                return meteoriteLandingsJson;
            }
        }
        static List<REST.MeteoriteLanding> restMeteoriteLandings;
        public static List<REST.MeteoriteLanding> RestMeteoriteLandings
        {
            get
            {
                if(restMeteoriteLandings == null)
                {
                    restMeteoriteLandings = JsonConvert.DeserializeObject<List<REST.MeteoriteLanding>>(MeteoriteLandingsJson);
                    var x = new object();
                }
                return restMeteoriteLandings;
            }
        }

        static List<MeteoriteLanding> grpcMeteoriteLandings = null;
        public static List<MeteoriteLanding> GrpcMeteoriteLandings
        {
            get
            {
                if (grpcMeteoriteLandings == null)
                {
                    grpcMeteoriteLandings = JsonConvert.DeserializeObject<List<MeteoriteLanding>>(MeteoriteLandingsJson, new ProtobufJsonConvertor());
                }
                return grpcMeteoriteLandings;
            }

        }
        static MeteoriteLandingsList grpcMeteoriteLandingsList;
        public static MeteoriteLandingsList GrpcMeteoriteLandingsList
        {
            get
            {
                if (grpcMeteoriteLandingsList == null)
                {
                    grpcMeteoriteLandingsList = new MeteoriteLandingsList();
                    grpcMeteoriteLandingsList.MeteoriteLandings.AddRange(GrpcMeteoriteLandings);

                }
                return grpcMeteoriteLandingsList;
            }
        }
    }
}
