using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reports.Model
{
	public class Visit
	{
		public int Id { get; set; }
		public decimal PhysicanFees { get; set; }
		public string Diagnosis { get; set; }
		public int PatientId { get; set; }
		public string PatientName { get; set; }
		public string PhysicanName { get; set; }
		public DateTime Date { get; set; }
	}
}