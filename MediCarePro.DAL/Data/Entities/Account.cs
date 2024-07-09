using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MediCarePro.DAL.Data.Entities
{
	public class Account : IdentityUser
	{
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PhoneNumber { get; set; }
        public int? SpecialtyId { get; set; }
        public Specialty? Specialty { get; set; }
        public ICollection<PhysicianSchedule> PhysicianSchedules { get; set; } = new HashSet<PhysicianSchedule>();
    }
}
