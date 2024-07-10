using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;
using MediCarePro.DAL.Interfaces;
using MediCarePro.DAL.Specifications;

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

		public async Task<Visit?> CreateVisitAsync(Visit visit)
		{
			var visitRepo = _unitOfWork.Repository<Visit>();

			visitRepo.Add(visit);
			await _unitOfWork.CompleteAsync();

			return visit;
		}

		public async Task<Patient?> GetPatientAsync(int id)
		{
			var patientRepo = _unitOfWork.Repository<Patient>();

			var patient = await patientRepo.GetAsync(id);
			
			return patient;
		}

		public async Task<IReadOnlyList<Patient>> GetPatientsAsync()
		{
			var patientRepo = _unitOfWork.Repository<Patient>();

			var patients = await patientRepo.GetAllAsync();

			return patients;
		}

		public async Task<PhysicianSchedule?> GetPhysicianScheduleAsync(int id)
		{
			var physicianScheduleRepo = _unitOfWork.Repository<PhysicianSchedule>();

			var physicianSchedule = await physicianScheduleRepo.GetAsync(id);

			return physicianSchedule;
		}

		public async Task<IReadOnlyList<Specialty>> GetSpecialtiesAsync()
		{
			var SpecialtyRepo = _unitOfWork.Repository<Specialty>();

			var Specialties = await SpecialtyRepo.GetAllAsync();

			return Specialties;
		}

	}
}
