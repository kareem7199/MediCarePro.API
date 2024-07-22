using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.DAL.Interfaces
{
	public interface IAccountRepository
	{
		public Task<IReadOnlyList<Account>> GetPhysiciansBySpecialtyAsync(int specialtyId);
		public Task<Account?> GetPhysicianAsync(string id);
	}
}
