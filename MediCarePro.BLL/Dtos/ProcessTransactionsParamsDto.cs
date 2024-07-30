using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.BLL.Dtos
{
	public class ProcessTransactionsParamsDto
	{
		[Required]
		[Range(1, int.MaxValue)]
        public int ItemId { get; set; }

		[Required]
		[Range(1, 2)]
		public InventoryStrategyDto InventoryStrategy { get; set; }
    }
}
