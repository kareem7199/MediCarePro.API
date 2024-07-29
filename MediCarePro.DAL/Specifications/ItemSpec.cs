using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.DAL.Specifications
{
	public class ItemSpec : BaseSpecifications<Item>
	{
        public ItemSpec(string itemName)
            :base(I => I.Name == itemName)
        {
            
        }
    }
}
