using AutoMapper;
using MediCarePro.API.Errors;
using MediCarePro.BLL.AccountService;
using MediCarePro.BLL.Dtos;
using MediCarePro.BLL.ReceptionScreenService;
using MediCarePro.DAL.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MediCarePro.API.Controllers
{
	[Authorize(Roles = "Reception")]
	public class ReceptionScreenController : BaseApiController
	{
		private readonly IReceptionScreenService _receptionScreenService;
		private readonly IAccountService _accountService;
		private readonly IMapper _mapper;

		public ReceptionScreenController(
			IReceptionScreenService receptionScreenService,
			IAccountService accountService,
			IMapper mapper)
		{
			_receptionScreenService = receptionScreenService;
			_accountService = accountService;
			_mapper = mapper;
		}

		[HttpGet("Specialty")]
		public async Task<ActionResult<IReadOnlyList<Specialty>>> GetSpecialties()
		{
			var Specialties = await _receptionScreenService.GetSpecialtiesAsync();

			return Ok(Specialties);
		}

		[HttpGet("Physician")]
		public async Task<ActionResult<IReadOnlyList<Account>>> GetPhysicians([FromQuery] int SpecialtyId)
		{
			var physicians = await _accountService.GetPhysiciansAsync(SpecialtyId);

			return Ok(_mapper.Map<IReadOnlyList<PhysicianDto>>(physicians));
		}

		[HttpGet("Patient")]
		public async Task<ActionResult<IReadOnlyList<Patient>>> GetPatients()
		{
			var patients = await _receptionScreenService.GetPatientsAsync();

			return Ok(_mapper.Map<IReadOnlyList<PatientForReceptionScreenDto>>(patients));
		}

		[HttpPost("Visit")]
		public async Task<ActionResult<Visit>> CreateVisit(VisitCreateDto model)
		{
			var schedule = await _receptionScreenService.GetPhysicianScheduleAsync(model.PhysicianScheduleId);
			if (schedule is null) return NotFound(new ApiResponse(404, "Schedule not found"));

			string dayName = model.Date.DayOfWeek.ToString();

			if (schedule.Day.ToString() != dayName || model.Date < DateOnly.FromDateTime(DateTime.Today)) return BadRequest(new ApiResponse(400, "Invalid Date"));

			if (schedule.AccountId != model.AccountId) return NotFound(new ApiResponse(404, "Physician not found"));

			var patient = await _receptionScreenService.GetPatientAsync(model.PatientId);
			if (patient is null) return NotFound(new ApiResponse(404, "Patient not found"));

			var mappedVisit = _mapper.Map<Visit>(model);

			var visit = await _receptionScreenService.CreateVisitAsync(mappedVisit);

			return Ok(visit);
		}


	}
}
