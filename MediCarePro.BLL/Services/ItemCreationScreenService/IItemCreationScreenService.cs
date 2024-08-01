using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.BLL.Services.ItemCreationScreenService
{
    public interface IItemCreationScreenService
    {
        public Task<Item?> CreateItemAsync(Item item);
    }
}
