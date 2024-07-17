using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;
using MediCarePro.DAL.Interfaces;
using MediCarePro.DAL.Specifications;
using Microsoft.AspNetCore.Mvc;

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

		public async Task<IReadOnlyList<PhysicianSchedule>> GetFilteredPhysicianScheduleAsync(string physicianId, TimeOnly startTime, Day day)
		{
			var physicianScheduleRepo = _unitOfWork.Repository<PhysicianSchedule>();
			var spec = new PhysicianScheduleSpec(physicianId, startTime, day);

			var physicianSchedule = await physicianScheduleRepo.GetAllWithSpecAsync(spec);

			return physicianSchedule;
		}

		public async Task<Patient?> GetPatientAsync(int id)
		{
			var patientRepo = _unitOfWork.Repository<Patient>();

			var patient = await patientRepo.GetAsync(id);
			
			return patient;
		}
		public async Task<IReadOnlyList<Patient>> GetPatientsAsync(string? searchTerm)
		{
			var patientRepo = _unitOfWork.Repository<Patient>();

			var spec = new PatientSpec(searchTerm);

			var patients = await patientRepo.GetAllWithSpecAsync(spec);

			return patients;
		}

		public async Task<IReadOnlyList<PhysicianSchedule>> GetPhysicianScheduleAsync(string id)
		{
			var physicianScheduleRepo = _unitOfWork.Repository<PhysicianSchedule>();

			var spec = new PhysicianScheduleSpec(id);

			var physicianSchedule = await physicianScheduleRepo.GetAllWithSpecAsync(spec);

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
