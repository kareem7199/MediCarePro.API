using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.DAL.Data.Entities
{
	public class Patient : BaseEntity
	{
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
    }
}
