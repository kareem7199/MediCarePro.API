using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;
using MediCarePro.DAL.Interfaces;
using MediCarePro.DAL.Specifications;

namespace MediCarePro.BLL.ItemCreationScreenService
{
	public class ItemCreationScreenService : IItemCreationScreenService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ItemCreationScreenService(
			IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}
        public async Task<Item?> CreateItemAsync(Item item)
		{
			var itemRepo = _unitOfWork.Repository<Item>();

			var spec = new ItemSpec(item.Name);

			var itemWithTheSameName = await itemRepo.GetWithSpecAsync(spec);

			if (itemWithTheSameName is not null) return null;

			itemRepo.Add(item);

			await _unitOfWork.CompleteAsync();

			return item;
		}
	}
}
