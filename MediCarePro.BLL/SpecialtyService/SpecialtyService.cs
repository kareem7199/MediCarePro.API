using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;
using MediCarePro.DAL.Interfaces;

namespace MediCarePro.BLL.SpecialtyService
{
	public class SpecialtyService : ISpecialtyService
	{
		private readonly IUnitOfWork _unitOfWork;

		public SpecialtyService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}

        public async Task<IReadOnlyList<Specialty>> GetSpecialtiesAsync()
		{
			var SpecialtyRepo = _unitOfWork.Repository<Specialty>();

			var Specialties = await SpecialtyRepo.GetAllAsync();

			return Specialties;
		}
	}
}
