using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.BLL.Dtos
{
	public class VisitDto
	{
        public int Id { get; set; }
        public double PhysicanFees { get; set; }
		public string PatientName { get; set; }
		public DateTime Date { get; set; }
	}
}
