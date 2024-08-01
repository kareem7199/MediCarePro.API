using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data;
using MediCarePro.DAL.Data.Entities;
using MediCarePro.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MediCarePro.BLL.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(
            IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<IReadOnlyList<Account>> GetPhysiciansAsync(int specialtyId)
        {
            var physicians = await _accountRepository.GetPhysiciansBySpecialtyAsync(specialtyId);

            return physicians;
        }
        public async Task<Account?> GetPhysicianAsync(string id)
        {
            var physician = await _accountRepository.GetPhysicianAsync(id);

            return physician;
        }
    }
}
