using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data;
using MediCarePro.DAL.Data.Entities;
using MediCarePro.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediCarePro.DAL.Repositories
{
	public class AccountRepository : IAccountRepository
	{
		private readonly StoreContext _storeContext;

		public AccountRepository(StoreContext storeContext)
        {
			_storeContext = storeContext;
		}
        public async Task<Account?> GetPhysicianAsync(string id)
		{
			var physician = await _storeContext.Users.FindAsync(id);

			return physician;
		}

		public async Task<IReadOnlyList<Account>> GetPhysiciansBySpecialtyAsync(int specialtyId)
		{
			var physicians = await _storeContext.Users.Where(A => A.SpecialtyId == specialtyId)
													  .ToListAsync();
			return physicians;
		}
	}
}
