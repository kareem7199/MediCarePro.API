using System.Security.Claims;
using AutoMapper;
using MediCarePro.API.Errors;
using MediCarePro.BLL.Dtos;
using MediCarePro.BLL.VisitService;
using MediCarePro.DAL.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCarePro.API.Controllers
{
	[Authorize(Roles = "Physician")]
	public class PhysicianScreenController : BaseApiController
	{
		private readonly IVisitService _visitService;
		private readonly IMapper _mapper;

		public PhysicianScreenController(
			IVisitService visitService,
			IMapper mapper)
		{
			_visitService = visitService;
			_mapper = mapper;
		}

		[HttpGet("Visit")]
		public async Task<ActionResult<IReadOnlyList<DailyVisitToReturnDto>>> GetDailyVisits([FromQuery] DailyVisitRequestDto model)
		{
			var physicianId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

			var visits = await _visitService.GetVisitsWithRangeAsync(physicianId, model.Date.Value, model.Date.Value.AddDays(1).AddSeconds(-1));

			return Ok(_mapper.Map<IReadOnlyList<DailyVisitToReturnDto>>(visits));
		}

		[HttpGet("Visit/{visitId}")]
		public async Task<ActionResult<VisitReportDto>> GetVisit(int visitId)
		{
			var physicianId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

			var visit = await _visitService.GetVisitByIdAsync(visitId, physicianId);

			if(visit is null) return NotFound(new ApiResponse(404 , "Visit not found"));

			return Ok(_mapper.Map<VisitReportDto>(visit));
		}

		[HttpPost("Visit")]
		public async Task<ActionResult<DiagnosisToReturnDto>> AddVisitDiagnosis(DiagnosisDto model)
		{
			var physicianId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

			var mappedDiagnosis = _mapper.Map<Diagnosis>(model);

			var diagnosis = await _visitService.CreateDiagnosisAsync(mappedDiagnosis , physicianId);

			if (diagnosis is null) return NotFound(new ApiResponse(404, "Visit not found."));

			return Ok(_mapper.Map<DiagnosisToReturnDto>(diagnosis));
		}

		[HttpPut("Visit/{id}")]
		public async Task<ActionResult<DiagnosisToReturnDto>> UpdateVisitDiagnosis(DiagnosisDto model , int id)
		{
			var physicianId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

			var mappedDiagnosis = _mapper.Map<Diagnosis>(model);
			mappedDiagnosis.Id = id;

			var diagnosis = await _visitService.UpdateVisitDiagnosisAsync(mappedDiagnosis , physicianId);

			if (diagnosis is null) return NotFound(new ApiResponse(404, "Diagnosis not found."));

			return Ok(_mapper.Map<DiagnosisToReturnDto>(diagnosis));
		}
	}
}
