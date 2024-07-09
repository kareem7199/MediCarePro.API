using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.DAL.Data.Entities
{
	public class PhysicianSchedule : BaseEntity
	{
        public Day Day { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }
		public string AccountId { get; set; }
        public Account Account { get; set; }
    }
}
