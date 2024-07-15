using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.DAL.Specifications
{
	public class PhysicianScheduleSpec : BaseSpecifications<PhysicianSchedule>
	{
        public PhysicianScheduleSpec(string physicianId)
            :base(PS => PS.AccountId == physicianId)
        {
            
        }
    }
}
