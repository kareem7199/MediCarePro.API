using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.BLL.Dtos
{
	public class DailyVisitRequestDto
	{
		[Required(ErrorMessage = "Date parameter is required.")]
		public DateTime? Date { get; set; }
	}
}
