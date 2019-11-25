namespace TransportSchedule.Classes.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TransportSchedule.Classes.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TransportSchedule.Classes.Context context)
        {
            Repository _repo = new Repository();

            foreach (var station in _repo.Stations)
            {
                context.Stations.AddOrUpdate(s=> s.Name, station);
            }

            foreach (var route in _repo.Routes)
            {
                foreach (var routestation in route.Stations)
                {
                    routestation.Station = context.Stations.FirstOrDefault(s => s.Name == routestation.Station.Name);
                    context.RouteStations.AddOrUpdate(rs=>rs.StationId, routestation);
                }
            }

            foreach (var user in _repo.Users)
            {
                user.Favourites = _repo.LoadUserFavourites(user.Id);
                foreach (var favourite in user.Favourites)
                {
                    context.Favourites.AddOrUpdate(f => f.Id, favourite);
                }
            }

            foreach (var user in _repo.Users)
            {
                context.Users.AddOrUpdate(u=> u.Login, user);
            }

            foreach (var route in _repo.Routes)
            {
                context.Routes.AddOrUpdate(r=>r.Name,route);
            }




            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
