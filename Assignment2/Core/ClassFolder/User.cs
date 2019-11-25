using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Try1.Core
{
    public class User
    {
        public string FullName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        //public List<string> FavStationsIds { get; set; }
        //[JsonIgnore] 
        public List<Station> FavStations { get; set; }
    }
}
