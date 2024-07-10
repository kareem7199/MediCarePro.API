using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.BLL.AccountService
{
	public interface IAccountService
	{
		public Task<IReadOnlyList<Account>> GetPhysiciansAsync(int specialityId);

	}
}
