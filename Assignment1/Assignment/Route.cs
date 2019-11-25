using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Route
    {
        public int ID { get; set; }
        public int FirstDepartureTime { get; set; }
        public int LastDeparturelTime { get; set; }
        public int Interval { get; set; }
        public List<string> StationsOnTheRoute { get; set; }
        public string Terminal { get; set; }
        public string Destination { get; set; }
    }
}
