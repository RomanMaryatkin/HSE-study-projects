using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Try1.Core
{
    public class Repository : IRepository
    {
        public List<Station> Stations { get; set; }
        public List<Route> Routes { get; set; }
        public List<User> Users { get; set; }

        public Repository()
        {
            //Stations = new List<Station>()
            //{
            //    new Station
            //    {
            //        Id=0,
            //        StationName="A",
            //        RoutsThroughTheStation=new List<int>{0, 1},
            //        TimeToTerminal =new List<int>{0, 12, 6, 8}
            //    },
            //    new Station
            //    {
            //        Id=1,
            //        StationName="B",
            //        RoutsThroughTheStation=new List<int>{0, 1},
            //        TimeToTerminal =new List<int>{3, 9, 9, 5}
            //    },
            //    new Station
            //    {
            //        Id=2,
            //        StationName="C",
            //        RoutsThroughTheStation=new List<int>{0},
            //        TimeToTerminal = new List<int>{8,4}
            //    },
            //    new Station
            //    {
            //        Id=3,
            //        StationName="D",
            //        RoutsThroughTheStation=new List<int>{0},
            //        TimeToTerminal = new List<int>{12, 0}
            //    },
            //    new Station
            //    {
            //        Id=4,
            //        StationName="E",
            //        RoutsThroughTheStation=new List<int>{1},
            //        TimeToTerminal = new List<int>{0,14}
            //    },
            //    new Station
            //    {
            //        Id=5,
            //        StationName="F",
            //        RoutsThroughTheStation=new List<int>{1},
            //        TimeToTerminal = new List<int>{14,0}
            //    },
            //};
            //Routes = new List<Route>
            //{
            //    new Route
            //    {
            //        ID = 0,
            //        Interval = 10,
            //        FirstDepartureTime = 300,
            //        LastDeparturelTime = 1380,
            //        StationsOnTheRoute =new List<string>{"A", "B", "C", "D"},
            //        Terminal ="A",
            //        Destination="D"
            //    },
            //    new Route
            //    {
            //        ID = 1,
            //        Interval = 15,
            //        FirstDepartureTime = 310,
            //        LastDeparturelTime = 85,
            //        StationsOnTheRoute =new List<string>{"A", "B", "C", "D"},
            //        Terminal ="E",
            //        Destination="F"
            //    }
            //};
            //Save();
            Restore();
        }

        private List<T> Restore<T>(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                using (var reader = new JsonTextReader(sr))
                {
                    var serialiser = new JsonSerializer();
                    return serialiser.Deserialize<List<T>>(reader);
                }
            }
        }

        private void Restore()
        {
            Stations = Restore<Station>("../../stations.json");
            Routes = Restore<Route>("../../routes.json");
            Users = Restore<User>("../../users.json");
            //foreach (var user in Users)
            //{
            //    if(user.FavStationsIds==null)
            //    user.FavStationsIds = new List<string>();
            //    foreach (string favStationId in user.FavStationsIds)
            //    {
            //        Stations.Add(Stations.FirstOrDefault(s => s.Id == favStationId));
            //    }
            //}
        }

        public void SaveList<T>(string fileName, List<T> list)
        {
            using (var sw = new StreamWriter(fileName))
            {
                using (var writer = new JsonTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;
                    var serializer = new JsonSerializer();
                    serializer.Serialize(writer, list);
                }
            }
        }

        public void Save()
        {
            //SaveList<Station>("../../stations.json", Stations);
            //SaveList<Route>("../../routes.json", Routes);
            SaveList<User>("../../users.json", Users);
            //using (var sw = new StreamWriter("stations.json"))
            //{
            //    using (var writer = new JsonTextWriter(sw))
            //    {
            //        writer.Formatting = Formatting.Indented;
            //        var serializer = new JsonSerializer();
            //        serializer.Serialize(writer, Stations);

            //    }
            //}
            //using (var sw = new StreamWriter("routes.json"))
            //{
            //    using (var writer = new JsonTextWriter(sw))
            //    {
            //        writer.Formatting = Formatting.Indented;
            //        var serializer = new JsonSerializer();
            //        serializer.Serialize(writer,Routes);
            //    }
            //}
        }
    }
}
