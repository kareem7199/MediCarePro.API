using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.BLL.ReceptionScreenService
{
	public interface IReceptionScreenService
	{
		public Task<IReadOnlyList<Specialty>> GetSpecialtiesAsync();
		public Task<IReadOnlyList<Patient>> GetPatientsAsync();
	}
}
