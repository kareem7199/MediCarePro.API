using MediCarePro.API.Errors;
using MediCarePro.BLL.Dtos;
using MediCarePro.BLL.InventoryService;
using MediCarePro.BLL.TransactionCreationScreenService;
using MediCarePro.DAL.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCarePro.API.Controllers
{
	[Authorize("InventoryManager")]
	public class InventoryAccountingScreenController : BaseApiController
	{
		private readonly IInventoryService _inventoryService;
		private readonly ITransactionCreationScreenService _transactionCreationScreenService;

		public InventoryAccountingScreenController(
			IInventoryService inventoryService,
			ITransactionCreationScreenService transactionCreationScreenService)
		{
			_inventoryService = inventoryService;
			_transactionCreationScreenService = transactionCreationScreenService;
		}

		[HttpGet("Item")]
		public async Task<ActionResult<IReadOnlyList<Item>>> GetItems()
		{
			var items = await _transactionCreationScreenService.GetItemsAsync();

			return Ok(items);
		}

		[HttpGet]
		public async Task<ActionResult> ProcessTransactions([FromQuery] ProcessTransactionsParamsDto paramsDto)
		{
			var inventoryValue = await _inventoryService.ProcessAllTransactionsAsync(paramsDto.ItemId, paramsDto.InventoryStrategy);

			if (inventoryValue is null) return BadRequest(new ApiResponse(400 , "Item not found"));

			return Ok(new ResultDto<decimal>()
			{
				Result = inventoryValue.Value
			});
		}
	}
}
