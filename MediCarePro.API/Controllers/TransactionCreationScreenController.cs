using System.Transactions;
using MediCarePro.API.Errors;
using MediCarePro.BLL.Dtos;
using MediCarePro.BLL.TransactionCreationScreenService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCarePro.API.Controllers
{
	public class TransactionCreationScreenController : BaseApiController
	{
		private readonly ITransactionCreationScreenService _transactionCreationScreenService;

		public TransactionCreationScreenController(
			ITransactionCreationScreenService transactionCreationScreenService)
		{
			_transactionCreationScreenService = transactionCreationScreenService;
		}

		[HttpPost]
		public async Task<ActionResult<Transaction>> CreateTransaction(CreateTransactionDto model)
		{
			var transaction = await _transactionCreationScreenService.CreateTransactionAsync(model.Amount, model.Quantity, model.Action, model.ItemId);

			if(transaction is null) return BadRequest(new ApiResponse(400 , "item not found"));

			return Ok(transaction);
		}
	}
}
