﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TransportSchedule.Classes.Helpers {
	public class PasswordHelpers {
		public static string GetHash(string password) {
			var md5 = MD5.Create();
			var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
			return Convert.ToBase64String(hash);
		}
	}
}
