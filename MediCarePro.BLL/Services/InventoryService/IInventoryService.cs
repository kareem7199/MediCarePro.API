using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.BLL.Dtos;
using MediCarePro.BLL.Strategies.InventoryStrategy;

namespace MediCarePro.BLL.Services.InventoryService
{
    public interface IInventoryService
    {
        public Task<decimal?> ProcessAllTransactionsAsync(int itemId, InventoryStrategyDto strategy);
    }
}
