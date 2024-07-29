using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.DAL.Data.Entities
{
	public class Transaction : BaseEntity
	{
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public int ItemId { get; set; }

        public Item Item { get; set; }
    }
}
