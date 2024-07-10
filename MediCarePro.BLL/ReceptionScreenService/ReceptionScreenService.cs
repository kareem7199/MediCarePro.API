using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;
using MediCarePro.DAL.Interfaces;

namespace MediCarePro.BLL.ReceptionScreenService
{
	public class ReceptionScreenService : IReceptionScreenService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ReceptionScreenService(
			IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}

		public async Task<IReadOnlyList<Patient>> GetPatientsAsync()
		{
			var patientRepo = _unitOfWork.Repository<Patient>();

			var patients = await patientRepo.GetAllAsync();

			return patients;
		}

		public async Task<IReadOnlyList<Specialty>> GetSpecialtiesAsync()
		{
			var SpecialtyRepo = _unitOfWork.Repository<Specialty>();

			var Specialties = await SpecialtyRepo.GetAllAsync();

			return Specialties;
		}
	}
}
