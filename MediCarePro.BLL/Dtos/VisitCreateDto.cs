using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.BLL.Dtos
{
	public class VisitCreateDto
	{
		[Required]
		[Range(0 , double.MaxValue)]
		public double PhysicanFees { get; set; }
		[Required]
		public string AccountId { get; set; }
		[Required]
		public int PatientId { get; set; }
		[Required]
		public int PhysicianScheduleId { get; set; }
		[Required]
		public DateTime Date { get; set; }
	}
}
