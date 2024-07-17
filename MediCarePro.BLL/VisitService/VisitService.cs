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

		public async Task<Visit?> UpdateVisitDiagnosisAsync(int VisitId, string diagnosis , string physicianId)
		{
			var visitRepo = _unitOfWork.Repository<Visit>();

			var visit = await visitRepo.GetAsync(VisitId);

			if (visit is null || visit.AccountId != physicianId) return null;

			visit.Diagnosis = diagnosis;

			visitRepo.Update(visit);
			await _unitOfWork.CompleteAsync();

			return visit;
		}
	}
}
