using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.BLL.VisitService
{
	public interface IVisitService
	{
		public Task<IReadOnlyList<Visit>> GetVisitsWithRangeAsync(string physicianId, DateTime from, DateTime to);
		public Task<Visit?> GetVisitByIdAsync(int VisitId, string physicianId);
		public Task<Visit?> CreateVisitAsync(Visit visit);
		//public Task<Visit?> UpdateVisitDiagnosisAsync(int VisitId , string diagnosis , string physicianId);
		public Task<Diagnosis?> CreateDiagnosisAsync(Diagnosis diagnosis , string physicianId);
	}
}
