using System.Transactions;
using MediCarePro.API.Errors;
using MediCarePro.BLL.Dtos;
using MediCarePro.BLL.TransactionCreationScreenService;
using MediCarePro.DAL.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCarePro.API.Controllers
{
	[Authorize("TransactionCreator")]
	public class TransactionCreationScreenController : BaseApiController
	{
		private readonly ITransactionCreationScreenService _transactionCreationScreenService;

		public TransactionCreationScreenController(
			ITransactionCreationScreenService transactionCreationScreenService)
		{
			_transactionCreationScreenService = transactionCreationScreenService;
		}

		[HttpPost("Transaction")]
		public async Task<ActionResult<DAL.Data.Entities.Transaction>> CreateTransaction(CreateTransactionDto model)
		{
			var transaction = await _transactionCreationScreenService.CreateTransactionAsync(model.Amount, model.Quantity, model.Action, model.ItemId);

			if(transaction is null) return BadRequest(new ApiResponse(400 , "item not found"));

			return Ok(transaction);
		}

		[HttpGet("Item")]
		public async Task<ActionResult<IReadOnlyList<Item>>> GetItems()
		{
			var items = await _transactionCreationScreenService.GetItemsAsync();

			return Ok(items);
		}
	}
}
