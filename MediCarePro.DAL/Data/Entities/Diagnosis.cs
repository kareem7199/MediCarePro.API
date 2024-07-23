using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.DAL.Data.Entities
{
	public class Diagnosis : BaseEntity
	{
        public int VisitId { get; set; }
		public string DiagnosisDetails { get; set; }
        public string BoneName { get; set; }
		public double Fees { get; set; }
        public Visit Visit { get; set; }
    }
}
