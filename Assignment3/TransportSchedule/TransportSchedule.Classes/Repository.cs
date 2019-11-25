using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TransportSchedule.Classes.Helpers;
using TransportSchedule.Classes.Interfaces;
using TransportSchedule.Classes.Models;

namespace TransportSchedule.Classes
{
	internal class Repository : IRepository
    {
		public class GeneralData
        {
			public List<Station> Stations { get; set; }
			public List<Route> Routes { get; set; }
			public List<User> Users { get; set; }
		}

		private const string DataFolder = "Data";
		private const string FileName = "ts.json";

		private GeneralData _generalData;
		private User _authorizedUser;

        public IEnumerable<Station> Stations => _generalData.Stations;
		public IEnumerable<Route> Routes => _generalData.Routes;
		public IEnumerable<Favourite> Favourites => _authorizedUser.Favourites;
        public IEnumerable<User> Users => _generalData.Users;

		public Repository()
        {
			//try {
                using (var sr = new StreamReader(Path.Combine(DataFolder, FileName)))
                {
                    using (var jsonReader = new JsonTextReader(sr))
                    {
                        var serializer = new JsonSerializer();
                        _generalData = serializer.Deserialize<GeneralData>(jsonReader);
                    }
                }

				foreach (var route in _generalData.Routes)
					foreach (var st in route.Stations) {
						st.Station = _generalData.Stations.First(s => s.Id == st.StationId);
					}
			//}
			//catch {
			//	// Is something goes wrong, start off with empty collections
			//	_generalData = new GeneralData {
			//		Users = new List<User>(),
			//		Stations = new List<Station>(),
			//		Routes = new List<Route>()
			//	};
			//}
		}

		public void RegisterUser(User user)
        {
			user.Id = _generalData.Users.Count > 0 ?  _generalData.Users.Max(u => u.Id) + 1 : 1;
			_generalData.Users.Add(user);
			Save();
		}

		public bool Authorize(string login, string password)
        {
			var user = _generalData.Users.FirstOrDefault(u => u.Login == login && u.Password == PasswordHelpers.GetHash(password));
			if (user != null) {
				user.Favourites = LoadUserFavourites(user.Id);
				_authorizedUser = user;
				return true;
			}
			return false;
		}

		public IEnumerable<ScheduleItem> GetSchedule(Station station) {
			List<ScheduleItem> result = new List<ScheduleItem>();

			// Call to DateTime.Now only once to prevent different readings on different loop iterations
			// Here manual time can also be set to test the algorithm
			DateTime currentDt = DateTime.Now;

			foreach (var route in _generalData.Routes)
            {
				var routeStation = route.Stations
					.FirstOrDefault(st => st.Station == station);

				if (routeStation != null)
                {

					if (routeStation != route.Stations.Last())
                    {
						int left = route.TimeToNextDepartureFromOrigin(routeStation, currentDt);
						result.Add(new ScheduleItem {
							RouteName = route.Name,
							Destination = route.Stations.Last().Station,
							MinutesLeft = left
						});
					}
					if (routeStation != route.Stations.First())
                    {
						int left = route.TimeToNextDepartureFromDest(routeStation, currentDt);
						result.Add(new ScheduleItem {
							RouteName = route.Name,
							Destination = route.Stations.First().Station,
							MinutesLeft = left
						});
					}
				}
			}
			return result;
		}

		private string GetUserFavouritesPath(int userId) => Path.Combine(DataFolder, userId.ToString() + ".json");

		public List<Favourite> LoadUserFavourites(int userId)
        {
			try
            {
                using (var sr = new StreamReader(GetUserFavouritesPath(userId)))
                {
                    using (var jsonReader = new JsonTextReader(sr))
                    {
                        var serializer = new JsonSerializer();
                        var favourites = serializer.Deserialize<List<Favourite>>(jsonReader);
                        foreach (var f in favourites)
                            f.Station = _generalData.Stations.First(st => st.Id == f.StationId);
                        return favourites;
                    }
                }
			}
			catch {
				return new List<Favourite>();
			}
		}

		public void Save()
        {
			if (!Directory.Exists(DataFolder))
            {
				Directory.CreateDirectory(DataFolder);				
			}
            using (var sw = new StreamWriter(Path.Combine(DataFolder, FileName)))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, _generalData);
                }
            }
		}

		public void SaveUserFavourites()
        {
            using (var sw = new StreamWriter(GetUserFavouritesPath(_authorizedUser.Id)))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, _authorizedUser.Favourites);
                }
            }
		}
	}
}
