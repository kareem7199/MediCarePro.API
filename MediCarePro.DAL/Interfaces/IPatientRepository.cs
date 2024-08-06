using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;
using Nest;

namespace MediCarePro.DAL.Interfaces
{
	public interface IPatientRepository
	{
		public Task<IReadOnlyList<Patient>> SearchPatientsAsync(string searchTerm);
	}
}
