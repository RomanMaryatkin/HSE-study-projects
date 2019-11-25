using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportSchedule.Classes.Models;

namespace TransportSchedule.Classes.Interfaces {
	public interface IRepository {
		IEnumerable<Station> Stations { get; }
		IEnumerable<Route> Routes { get; }
		IEnumerable<Favourite> Favourites { get; }
        //IEnumerable<User> Users { get; }
		bool Authorize(string login, string password);
		void RegisterUser(User user);		

		IEnumerable<ScheduleItem> GetSchedule(Station station);
	}
}
