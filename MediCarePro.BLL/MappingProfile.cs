using AutoMapper;
using MediCarePro.BLL.Dtos;
using MediCarePro.BLL.Resolvers;
using MediCarePro.DAL.Data.Entities;
using Microsoft.AspNetCore.Identity;

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

			CreateMap<Visit, VisitDto>()
				.ForMember(D => D.PatientName, O => O.MapFrom(S => S.Patient.Name));

			CreateMap<Visit, DailyVisitToReturnDto>()
				.ForMember(D => D.PatientName, O => O.MapFrom(S => S.Patient.Name));

			CreateMap<Visit, VisitReportDto>()
					.ForMember(D => D.PatientName, O => O.MapFrom(S => S.Patient.Name))
					.ForMember(D => D.PhysicanName , O => O.MapFrom(S => $"{S.Physician.FirstName} {S.Physician.SecondName}"));

			CreateMap<IdentityRole, RoleDto>();
		}
	}
}
