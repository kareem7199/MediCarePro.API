using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;
using MediCarePro.DAL.Interfaces;
using MediCarePro.DAL.Specifications;

namespace MediCarePro.BLL.VisitService
{
	public class VisitService : IVisitService
	{
		private readonly IUnitOfWork _unitOfWork;

		public VisitService(
			IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}
        public async Task<IReadOnlyList<Visit>> GetVisitsWithRangeAsync(string physicianId, DateTime from, DateTime to)
		{
			var visitRepo = _unitOfWork.Repository<Visit>();
			var spec = new VisitSpec(physicianId, from, to);

			var visits = await visitRepo.GetAllWithSpecAsync(spec);

			return visits;
		}

		public async Task<Visit?> CreateVisitAsync(Visit visit)
		{
			var visitRepo = _unitOfWork.Repository<Visit>();

			visitRepo.Add(visit);
			await _unitOfWork.CompleteAsync();

			return visit;
		}

		//public async Task<Visit?> UpdateVisitDiagnosisAsync(int VisitId, string diagnosis , string physicianId)
		//{
		//	var visitRepo = _unitOfWork.Repository<Visit>();

		//	var visit = await visitRepo.GetAsync(VisitId);

		//	if (visit is null || visit.AccountId != physicianId) return null;

		//	visit.Diagnosis = diagnosis;

		//	visitRepo.Update(visit);
		//	await _unitOfWork.CompleteAsync();

		//	return visit;
		//}

		public async Task<Visit?> GetVisitByIdAsync(int VisitId, string physicianId)
		{
			var visitRepo = _unitOfWork.Repository<Visit>();

			var spec = new VisitSpec(VisitId);

			var visit = await visitRepo.GetWithSpecAsync(spec);

			if (visit is null || visit.AccountId != physicianId) return null;

			return visit;
		}

		public async Task<Diagnosis?> CreateDiagnosisAsync(Diagnosis diagnosis , string physicianId)
		{
			var visitRepo = _unitOfWork.Repository<Visit>();
			var diagnosisRepo = _unitOfWork.Repository<Diagnosis>();


			var visit = await visitRepo.GetAsync(diagnosis.VisitId);

			if (visit is null || visit.AccountId != physicianId) return null;

			diagnosisRepo.Add(diagnosis);
			await _unitOfWork.CompleteAsync();

			return diagnosis;
		}

		public async Task<Diagnosis?> UpdateVisitDiagnosisAsync(Diagnosis diagnosis, string physicianId)
		{
			var diagnosisRepo = _unitOfWork.Repository<Diagnosis>();

			var visit = await GetVisitByIdAsync(diagnosis.VisitId, physicianId);

			if (visit is null || visit.AccountId != physicianId) return null;

			var existingDiagnosis = await diagnosisRepo.GetAsync(diagnosis.Id);

			if (existingDiagnosis is null || diagnosis.BoneName != existingDiagnosis.BoneName) return null;

			existingDiagnosis.DiagnosisDetails = diagnosis.DiagnosisDetails;
			existingDiagnosis.Procedure = diagnosis.Procedure;
			existingDiagnosis.Fees = diagnosis.Fees;

			diagnosisRepo.Update(existingDiagnosis);
			await _unitOfWork.CompleteAsync();

			return existingDiagnosis;
		}
	}
}
