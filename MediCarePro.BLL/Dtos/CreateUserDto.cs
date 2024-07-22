using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.BLL.Dtos
{
	public class CreateUserDto
	{
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string SecondName { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		public int? SpecialtyId { get; set; }
		[Required]
		public string[] Roles { get; set; }
    }
}
