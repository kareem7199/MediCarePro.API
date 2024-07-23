using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.BLL.Dtos
{
	public class DiagnosisDto
	{
		[Required]
        public string BoneName { get; set; }
		[Required]
		public int VisitId { get; set; }
		[Required]
		[Range(0, double.MaxValue)]
		public double Fees { get; set; }
		[Required]
		public string DiagnosisDetails { get; set; }

	}
}
