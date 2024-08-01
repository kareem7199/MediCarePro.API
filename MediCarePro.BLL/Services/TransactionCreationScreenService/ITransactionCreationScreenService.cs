using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.BLL.Dtos;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.BLL.Services.TransactionCreationScreenService
{
    public interface ITransactionCreationScreenService
    {
        public Task<Transaction?> CreateTransactionAsync(decimal amount, int quantity, TransactionActionDto action, int itemId);
        public Task<IReadOnlyList<Item>> GetItemsAsync();
    }
}
