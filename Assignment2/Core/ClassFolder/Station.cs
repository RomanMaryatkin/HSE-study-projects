using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Try1.Core
{
    public class Station
    {
        public string Id { get; set; }
        public string StationName { get; set; }
        public List<int> RoutsThroughTheStation { get; set; }
        public List<int> TimeToTerminal { get; set; }
    }
}
