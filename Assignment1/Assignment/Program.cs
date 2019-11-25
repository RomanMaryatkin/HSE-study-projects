using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            string theStation;
            do
            {
                Console.Write("Enter station: ");
                theStation = Console.ReadLine().ToUpper().Trim();
                DateTime currentTime = DateTime.Now;
                int currentTimeInMinutes = currentTime.Hour * 60 + currentTime.Minute;
                List<Station> stations = new List<Station>();
                List<Route> routs = new List<Route>();
                int stId = 0;
                int rtId = 0;
                using (var sr = new StreamReader("../../info.txt"))
                {
                    int numberOfRouts = int.Parse(sr.ReadLine());
                    int numberOfStations = int.Parse(sr.ReadLine());
                    for (int i = 0; i < numberOfStations; i++)
                    {
                        var station = new Station
                        {
                            Id = stId++,
                            StationName = sr.ReadLine(),
                            RoutsThroughTheStation = sr.ReadLine().Split(';').Select(x => int.Parse(x)).ToList(),
                            TimeToTerminal = sr.ReadLine().Split(';').Select(x => int.Parse(x)).ToList()
                        };
                        stations.Add(station);
                    }
                    for (int j = 0; j < numberOfRouts; j++)
                    {
                        var route = new Route
                        {
                            ID = rtId++,
                            Interval = int.Parse(sr.ReadLine()),
                            FirstDepartureTime = int.Parse(sr.ReadLine()),
                            LastDeparturelTime = int.Parse(sr.ReadLine()),
                            StationsOnTheRoute = sr.ReadLine().Split(';').ToList(),
                            Terminal = sr.ReadLine(),
                            Destination = sr.ReadLine()
                        };
                        routs.Add(route);
                    }
                    List<string> stationsToCompare = new List<string>();
                    int timeInCaseItsToSmall;
                    foreach (var station in stations)
                    {
                        stationsToCompare.Add(station.StationName);
                        if (stationsToCompare.Any(x=>x==theStation))
                        {
                            if (theStation == station.StationName)
                            {
                                Console.WriteLine($"Current time is {DateTime.Now.ToShortTimeString()}");
                                Console.WriteLine("Schedule:");
                                foreach (var routethroughstation in station.RoutsThroughTheStation)
                                {
                                    foreach (var route in routs)
                                    {
                                        if (route.FirstDepartureTime>route.LastDeparturelTime)
                                        {
                                            route.LastDeparturelTime += 1440;
                                        }
                                        if ((currentTimeInMinutes<route.LastDeparturelTime) &&(currentTimeInMinutes < route.FirstDepartureTime))
                                        {
                                            timeInCaseItsToSmall = currentTimeInMinutes + 1440;
                                        }
                                        else
                                        {
                                            timeInCaseItsToSmall = currentTimeInMinutes;
                                        }
                                        if (routethroughstation == route.ID)
                                        {
                                            if ((timeInCaseItsToSmall >= route.FirstDepartureTime)&&(timeInCaseItsToSmall <= route.LastDeparturelTime))
                                            {
                                                for (int k = 0; k < station.RoutsThroughTheStation.Count; k += 2)
                                                {
                                                    Console.WriteLine($"{route.ID}, Arriving in " +
                                                        $"{(-currentTimeInMinutes + station.TimeToTerminal[k] + route.FirstDepartureTime + 1440 * route.Interval) % route.Interval} min," +
                                                        $" Destination is {route.Destination}");
                                                    Console.WriteLine($"{route.ID}, Arriving in " +
                                                        $"{(-currentTimeInMinutes + station.TimeToTerminal[k + 1] + route.FirstDepartureTime + 1440 * route.Interval) % route.Interval} min," +
                                                        $" Destination is {route.Terminal}");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine($"Buses on the route {route.ID} are not going for for a while");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (stationsToCompare.Any(x => x == theStation))
                    {
                        
                    }
                    else
                    {
                        if (theStation != "")
                        {
                            Console.WriteLine("No such station");
                        }
                    }
                }
            } while (theStation!="");
            Environment.Exit(0);
            Console.ReadLine();
        }
    }
}
