using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TransportSchedule.Classes.Models {
	public class User {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }

		[JsonIgnore]
		public List<Favourite> Favourites { get; set; }

	}
}
