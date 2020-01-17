using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace GrpcServer.Data
{
    public class MeteoriteLandingData
    {
        static string meteoriteLandingsJson = null;
        public static string MeteoriteLandingsJson
        {
            get
            {
                if (meteoriteLandingsJson == null)
                {
                    var dir = Environment.CurrentDirectory;
                    using (var reader = new StreamReader($"{dir}\\Data\\MeteoriteLandings.json"))
                    {
                        meteoriteLandingsJson = reader.ReadToEnd();
                    }
                }
                return meteoriteLandingsJson;
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
