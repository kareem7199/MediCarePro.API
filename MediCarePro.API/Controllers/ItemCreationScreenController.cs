using AutoMapper;
using MediCarePro.API.Errors;
using MediCarePro.BLL.Dtos;
using MediCarePro.BLL.ItemCreationScreenService;
using MediCarePro.DAL.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MediCarePro.API.Controllers
{
	[Authorize("ItemCreator")]
	public class ItemCreationScreenController : BaseApiController
	{
		private readonly IMapper _mapper;
		private readonly IItemCreationScreenService _itemCreationScreenService;

		public ItemCreationScreenController(
			IMapper mapper,
			IItemCreationScreenService itemCreationScreenService)
        {
			_mapper = mapper;
			_itemCreationScreenService = itemCreationScreenService;
		}

		[HttpPost]
		public async Task<ActionResult<Item>> CreateItemAsync(CreateItemDto model)
		{
			var mappedItem = _mapper.Map<Item>(model);

			var item = await _itemCreationScreenService.CreateItemAsync(mappedItem);

			if (item is null) return BadRequest(new ApiResponse(400, "Item with the given name already exists."));

			return Ok(item);
		}
    }
}
