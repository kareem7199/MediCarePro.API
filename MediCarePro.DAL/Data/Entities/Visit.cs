using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.DAL.Data.Entities
{
	public class Visit : BaseEntity
	{
		public double PhysicanFees { get; set; }
        public string AccountId { get; set; }
        public int PatientId { get; set; }
        public int PhysicianScheduleId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Diagnosis> Diagnoses { get; set; } = new HashSet<Diagnosis>();
        public PhysicianSchedule PhysicianSchedule { get; set; }
        public Patient Patient { get; set; }
        public Account Physician { get; set; }
    }
}
