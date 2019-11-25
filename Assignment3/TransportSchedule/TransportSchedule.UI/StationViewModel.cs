using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportSchedule.Classes.Models;

namespace TransportSchedule.UI {
	class StationViewModel
    {
		public Favourite Favourite { get; set; }
		public Station Station { get; set; }

		public string Name => Favourite != null ? $"{Favourite.Description} ({Station.Name})" : Station.Name;
	}
}
