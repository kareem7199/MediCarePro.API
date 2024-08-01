using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.BLL.Dtos;
using MediCarePro.DAL.Data.Entities;
using MediCarePro.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace MediCarePro.BLL.Services.TransactionCreationScreenService
{
    public class TransactionCreationScreenService : ITransactionCreationScreenService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionCreationScreenService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Transaction?> CreateTransactionAsync(decimal amount, int quantity, TransactionActionDto action, int itemId)
        {
            var itemRep = _unitOfWork.Repository<Item>();

            var item = await itemRep.GetAsync(itemId);
            if (item is null) return null;

            var transactionRepo = _unitOfWork.Repository<Transaction>();

            var transaction = new Transaction
            {
                ItemId = itemId,
                Quantity = quantity,
                Amount = action == TransactionActionDto.In ? -1 * amount : amount
            };

            transactionRepo.Add(transaction);

            await _unitOfWork.CompleteAsync();

            return transaction;
        }

        public async Task<IReadOnlyList<Item>> GetItemsAsync()
        {
            var itemRepo = _unitOfWork.Repository<Item>();

            var items = await itemRepo.GetAllAsync();

            return items;
        }
    }
}
