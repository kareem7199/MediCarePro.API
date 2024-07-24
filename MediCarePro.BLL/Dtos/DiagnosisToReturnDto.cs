using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.BLL.Dtos
{
	public class DiagnosisToReturnDto
	{
        public int Id { get; set; }
		public string BoneName { get; set; }
		public double Fees { get; set; }
		public string Procedure { get; set; }
		public string DiagnosisDetails { get; set; }
	}
}
