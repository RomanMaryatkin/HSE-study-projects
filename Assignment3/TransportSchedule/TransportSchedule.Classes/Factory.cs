using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportSchedule.Classes.Interfaces;

namespace TransportSchedule.Classes {
	public class Factory
    {
		private static Factory _instance;

		public static Factory Instance => _instance ?? (_instance = new Factory());

		private IRepository _repo;

		public IRepository GetRepository() => _repo ?? (_repo = new Repository());

        public IRepository GetDbRepository() => _repo ?? (_repo = new DbRepository());
	}
}
