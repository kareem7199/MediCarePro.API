using MediCarePro.API.Errors;
using MediCarePro.BLL.Dtos;
using MediCarePro.BLL.InventoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCarePro.API.Controllers
{
	[Authorize("InventoryManager")]
	public class InventoryAccountingScreenController : BaseApiController
	{
		private readonly IInventoryService _inventoryService;

		public InventoryAccountingScreenController(IInventoryService inventoryService)
		{
			_inventoryService = inventoryService;
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
