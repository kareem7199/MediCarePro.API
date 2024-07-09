using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.BLL.AuthService
{
	public interface IAuthService
	{
		Task<string> CreateTokenAsync(Account user);
	}
}
