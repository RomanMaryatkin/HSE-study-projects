using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Try1.Core;

namespace Core
{
    public interface IRepository
    {
         List<Station> Stations { get; }
         List<Route> Routes { get; }
         List<User> Users { get; }
    }
}
