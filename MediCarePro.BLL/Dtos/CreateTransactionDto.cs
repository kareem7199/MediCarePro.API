using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.BLL.Dtos
{
	public class CreateTransactionDto
	{
		[Required]
		[Range(.01 , double.MaxValue)]
		public decimal Amount { get; set; }
		[Required]
		[Range (1 , int.MaxValue)]
		public int Quantity { get; set; }
		[Required]
		public TransactionActionDto Action { get; set; }
		[Required]
		public int ItemId { get; set; }
	}
}
