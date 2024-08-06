using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data;
using MediCarePro.DAL.Data.Entities;
using MediCarePro.DAL.Interfaces;
using MediCarePro.DAL.Specifications;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Day = MediCarePro.DAL.Data.Entities.Day;

namespace MediCarePro.BLL.Services.ReceptionScreenService
{
	public class ReceptionScreenService : IReceptionScreenService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPatientRepository _patientRepository;
		
		public ReceptionScreenService(
			IUnitOfWork unitOfWork,
			IPatientRepository patientRepository)
		{
			_unitOfWork = unitOfWork;
			_patientRepository = patientRepository;
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

			var patients = await _patientRepository.SearchPatientsAsync(searchTerm);

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
