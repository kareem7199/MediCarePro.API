using AutoMapper;
using MediCarePro.BLL.Dtos;
using MediCarePro.BLL.Resolvers;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.BLL
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Patient, PatientForReceptionScreenDto>();

			CreateMap<Account, PhysicianDto>()
				.ForMember(D => D.Name, O => O.MapFrom<FullNameResolver>());

			CreateMap<VisitCreateDto, Visit>();

			CreateMap<PhysicianSchedule, PhysicianScheduleDto>();

			CreateMap<Visit ,  VisitDto>()
				.ForMember(D => D.PatientName , O => O.MapFrom(S => S.Patient.Name));
		}
	}
}
