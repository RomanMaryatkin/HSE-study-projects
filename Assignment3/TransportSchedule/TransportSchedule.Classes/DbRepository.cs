using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportSchedule.Classes.Helpers;
using TransportSchedule.Classes.Interfaces;
using TransportSchedule.Classes.Models;

namespace TransportSchedule.Classes
{
    class DbRepository : IRepository
    {
        private Context context = new Context();
        private User _authorizedUser;

        public IEnumerable<Station> Stations => context.Stations;
        public IEnumerable<Route> Routes => context.Routes.Include("Stations").ToList();
        public IEnumerable<Favourite> Favourites => _authorizedUser.Favourites;
        //public IEnumerable<User> Users => context.Users;

        public void RegisterUser(User user)
        {
            user.Id = context.Users.Count() > 0 ? context.Users.Max(u => u.Id) + 1 : 1;
            context.Users.Add(user);
            context.SaveChanges();
        }

        private List<Favourite> LoadUserFavourites(int userId)
        {
            try
            {
                var user = context.Users.Include("Favourites").FirstOrDefault(u => u.Id == userId);
                var favourites = user.Favourites;
                foreach (var f in favourites)
                    f.Station = context.Stations.First(st => st.Id == f.StationId);
                return favourites.ToList();
            }
            catch
            {
                return new List<Favourite>();
            }
        }

        public bool Authorize(string login, string password)
        {
            var user = context.Users.ToList().FirstOrDefault(u => u.Login == login && u.Password == PasswordHelpers.GetHash(password));
            if (user != null)
            {
                user.Favourites = LoadUserFavourites(user.Id);
                _authorizedUser = user;
                return true;
            }
            return false;
        }

        public IEnumerable<ScheduleItem> GetSchedule(Station station)
        {
            List<ScheduleItem> result = new List<ScheduleItem>();
            DateTime currentDt = DateTime.Now;

            foreach (var route in Routes)
            {
                var routeStation = route.Stations
                    .FirstOrDefault(st => st.Station == station);

                if (routeStation != null)
                {

                    if (routeStation != route.Stations.Last())
                    {
                        int left = route.TimeToNextDepartureFromOrigin(routeStation, currentDt);
                        result.Add(new ScheduleItem
                        {
                            RouteName = route.Name,
                            Destination = route.Stations.Last().Station,
                            MinutesLeft = left
                        });
                    }
                    if (routeStation != route.Stations.First())
                    {
                        int left = route.TimeToNextDepartureFromDest(routeStation, currentDt);
                        result.Add(new ScheduleItem
                        {
                            RouteName = route.Name,
                            Destination = route.Stations.First().Station,
                            MinutesLeft = left
                        });
                    }
                }
            }
            return result;
        }
        public void AddFavourite(Favourite favourite)
        {
            if(context.Favourites.FirstOrDefault(f=>f.StationId==favourite.StationId)==null)
                 context.Favourites.Add(favourite);
        }
        public void DeleteFavourite(Favourite favourite)
        {
            if (context.Favourites.FirstOrDefault(f => f.StationId == favourite.StationId) != null)
                context.Favourites.Remove(favourite);
        }
        public void EditFavourite(Favourite favourite, string description)
        {
            if (context.Favourites.FirstOrDefault(f => f.StationId == favourite.StationId) != null)
                context.Favourites.FirstOrDefault(f => f.StationId == favourite.StationId).Description = description;
        }
    }
}
