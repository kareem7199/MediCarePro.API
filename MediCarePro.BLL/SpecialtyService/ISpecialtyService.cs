using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.BLL.SpecialtyService
{
	public interface ISpecialtyService
	{
		public Task<IReadOnlyList<Specialty>> GetSpecialtiesAsync();
	}
}
