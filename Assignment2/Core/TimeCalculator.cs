using Core.ClassFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Try1.Core;

namespace Core
{
    public class TimeCalculator
    {
        Repository _repository { get; set; }
        //public string theStation;

        public int GetTime()
        {
            DateTime currentTime = DateTime.Now;
            int currentTimeInMinutes = currentTime.Hour * 60 + currentTime.Minute;
            return currentTimeInMinutes;
        }

        public List<CopeEverithingForDataGrid> TheAnswers (Repository repository, Station theChosenStation)
        {
            _repository = repository;
            var theAnswers = new List<CopeEverithingForDataGrid>();
            int timeToDestination = 0;
            int timeToTerminal = 0;
            List<string> stationsToCompare = new List<string>();
            int timeInCaseItsToSmall;
            foreach (var station in _repository.Stations)
            {
                stationsToCompare.Add(theChosenStation.StationName);
                if (stationsToCompare.Any(x => x == theChosenStation.StationName))
                {
                    if (theChosenStation.StationName == station.StationName)
                    {
                        foreach (var routethroughstation in station.RoutsThroughTheStation)
                        {
                            foreach (var route in _repository.Routes)
                            {
                                if (route.FirstDepartureTime > route.LastDeparturelTime)
                                {
                                    route.LastDeparturelTime += 1440;
                                }
                                if ((GetTime() < route.LastDeparturelTime) && (GetTime() < route.FirstDepartureTime))
                                {
                                    timeInCaseItsToSmall = GetTime() + 1440;
                                }
                                else
                                {
                                    timeInCaseItsToSmall = GetTime();
                                }
                                if (routethroughstation == route.ID)
                                {
                                    if ((timeInCaseItsToSmall >= route.FirstDepartureTime) && (timeInCaseItsToSmall <= route.LastDeparturelTime))
                                    {
                                        for (int k = 0; k < station.RoutsThroughTheStation.Count; k += 2)
                                        {
                                            timeToDestination = (-GetTime() + station.TimeToTerminal[k] + route.FirstDepartureTime + 1440 * route.Interval) % route.Interval; //destination is destination
                                            timeToTerminal = (-GetTime() + station.TimeToTerminal[k + 1] + route.FirstDepartureTime + 1440 * route.Interval) % route.Interval; //Destination is {route.Terminal}");
                                            theAnswers.Add(new CopeEverithingForDataGrid {RouteId = route.ID, TimeToDestination = timeToDestination, TimeToTerminal = timeToTerminal, DestinationName = route.Destination, TerminalName = route.Terminal });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return theAnswers;
        }
    }
}
