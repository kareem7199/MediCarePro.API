using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data;
using MediCarePro.DAL.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MediCarePro.BLL.AccountService
{
	public class AccountService : IAccountService
	{
		private readonly UserManager<Account> _userManager;
		private readonly StoreContext _storeContext;

		public AccountService(
			UserManager<Account> userManager,
			StoreContext storeContext)
		{
			_userManager = userManager;
			_storeContext = storeContext;
		}
		public async Task<IReadOnlyList<Account>> GetPhysiciansAsync(int specialtyId)
		{
			var physicians = await _storeContext.Users.Where(A => A.SpecialtyId == specialtyId)
												      .ToListAsync();
			return physicians;
		}
		public async Task<Account?> GetPhysicianAsync(string id)
		{
			var physician = await _storeContext.Users.FindAsync(id);
			return physician;
		}
	}
}
