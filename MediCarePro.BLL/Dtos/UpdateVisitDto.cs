using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.BLL.Dtos
{
	public class UpdateVisitDto
	{
		[Required]	
		public int Id { get; set; }
		[Required]
		public string Diagnosis { get; set; }
	}
}
