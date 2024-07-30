using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.BLL.Dtos;
using MediCarePro.BLL.Strategies.InventoryStrategy;
using MediCarePro.DAL.Data.Entities;
using MediCarePro.DAL.Interfaces;
using MediCarePro.DAL.Specifications;

namespace MediCarePro.BLL.InventoryService
{
	public class InventoryService : IInventoryService
	{
		private readonly IUnitOfWork _unitOfWork;
		private IInventoryStrategy _strategy;

		public InventoryService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		private void SetStrategy(IInventoryStrategy strategy)
		{
			_strategy = strategy;
		}

		public async Task<decimal?> ProcessAllTransactionsAsync(int itemId, InventoryStrategyDto strategy)
		{

			var itemRepo = _unitOfWork.Repository<Item>();
			var item = await itemRepo.GetAsync(itemId);

			if (item is null) return null;

			var transactionRepo = _unitOfWork.Repository<Transaction>();

			switch (strategy)
			{
				case InventoryStrategyDto.FIFO:
					SetStrategy(new FifoStrategy());
					break;
				default:
					SetStrategy(new LifoStrategy());
					break;
			}

			var spec = new TransactionSpec(itemId);
			var transactions = await transactionRepo.GetAllWithSpecAsync(spec);

			var inventoryValue = _strategy.ProcessTransactionAsync(transactions.ToList());

			return inventoryValue;
		}
	}
}
