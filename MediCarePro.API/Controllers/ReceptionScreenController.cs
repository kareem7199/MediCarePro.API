using AutoMapper;
using MediCarePro.API.Errors;
using MediCarePro.API.Hubs;
using MediCarePro.BLL.AccountService;
using MediCarePro.BLL.Dtos;
using MediCarePro.BLL.ReceptionScreenService;
using MediCarePro.BLL.VisitService;
using MediCarePro.DAL.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MediCarePro.API.Controllers
{
	[Authorize(Roles = "Reception")]
	public class ReceptionScreenController : BaseApiController
	{
		private readonly IReceptionScreenService _receptionScreenService;
		private readonly IAccountService _accountService;
		private readonly IVisitService _visitService;
		private readonly IMapper _mapper;
		private readonly IHubContext<VisitHub> _hubContext;

		public ReceptionScreenController(
			IReceptionScreenService receptionScreenService,
			IAccountService accountService,
			IVisitService visitService,
			IMapper mapper,
			IHubContext<VisitHub> hubContext)
		{
			_receptionScreenService = receptionScreenService;
			_accountService = accountService;
			_visitService = visitService;
			_mapper = mapper;
			_hubContext = hubContext;
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
		public async Task<ActionResult<IReadOnlyList<Patient>>> GetPatients([FromQuery] string? searchTerm)
		{
			var patients = await _receptionScreenService.GetPatientsAsync(searchTerm);

			return Ok(_mapper.Map<IReadOnlyList<PatientForReceptionScreenDto>>(patients));
		}

		[HttpGet("Schedule/{physicianId}")]
		public async Task<ActionResult<PhysicianScheduleDto>> GetPhysicianSchedule(string physicianId)
		{
			var schedule = await _receptionScreenService.GetPhysicianScheduleAsync(physicianId);

			return Ok(_mapper.Map<IReadOnlyList<PhysicianScheduleDto>>(schedule));
		}

		[HttpGet("Visit/{physicianId}")]
		public async Task<ActionResult<IReadOnlyList<VisitDto>>> GetVisits([FromQuery] GetVisitsParamsDto getVisitsParams, string physicianId)
		{
			var visits = await _visitService.GetVisitsWithRangeAsync(physicianId, getVisitsParams.From, getVisitsParams.To.AddSeconds(-1));

			return Ok(_mapper.Map<IReadOnlyList<VisitDto>>(visits));
		}

		[HttpPost("Visit")]
		public async Task<ActionResult<VisitDto>> CreateVisit(VisitCreateDto model)
		{
			string dayName = model.Date.DayOfWeek.ToString();

			if (model.Date < DateTime.Today) return BadRequest(new ApiResponse(400, "Date cannot be in the past"));

			var visits = await _visitService.GetVisitsWithRangeAsync(model.AccountId, model.Date.AddSeconds(-1), model.Date.AddSeconds(29 * 60 + 59));
			if (visits.Any()) return BadRequest(new ApiResponse(400, "This slot is already booked by another patient"));

			var startTime = TimeOnly.FromDateTime(model.Date);

			var schedule = await _receptionScreenService.GetFilteredPhysicianScheduleAsync(model.AccountId, startTime, (Day)Enum.Parse(typeof(Day), dayName));
			if (!schedule.Any()) return BadRequest(new ApiResponse(400, "This slot is outside the physician's available hours"));

			var patient = await _receptionScreenService.GetPatientAsync(model.PatientId);
			if (patient is null) return NotFound(new ApiResponse(404, "Patient not found"));


			var mappedVisit = _mapper.Map<Visit>(model);
			mappedVisit.AccountId = model.AccountId;

			var visit = await _visitService.CreateVisitAsync(mappedVisit);

			await _hubContext.Clients.User(model.AccountId).SendAsync("ReceiveVisit", new VisitNotificationDto() {
				Date = DateOnly.FromDateTime(visit.Date) ,
				Visit = _mapper.Map<DailyVisitToReturnDto>(visit)
			});

			return Ok(_mapper.Map<VisitDto>(visit));
		}
	}
}
