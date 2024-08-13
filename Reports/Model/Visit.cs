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
		public List<Diagnosis> Diagnoses { get; set; }
		public int PatientId { get; set; }
		public string PatientName { get; set; }
		public string PhysicanName { get; set; }
		public DateTime Date { get; set; }
	}
	public class Diagnosis
	{
        public decimal Fees { get; set; }
        public string Procedure { get; set; }
		public string BoneName { get; set; }
		public string DiagnosisDetails { get; set; }
	}
}