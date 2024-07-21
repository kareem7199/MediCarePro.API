using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reports.Model
{
	public class LoginResponse
	{
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}