using System.Security.Claims;
using AutoMapper;
using MediCarePro.API.Errors;
using MediCarePro.BLL.Dtos;
using MediCarePro.BLL.VisitService;
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

			var visits = await _visitService.GetVisitsWithRangeAsync(physicianId , model.Date.Value , model.Date.Value.AddDays(1).AddSeconds(-1));

			return Ok(_mapper.Map<IReadOnlyList<DailyVisitToReturnDto>>(visits));
		}

		[HttpPatch("Visit")]
		public async Task<ActionResult<DailyVisitToReturnDto>> UpdateVisitDiagnosis(UpdateVisitDto model)
		{
			var physicianId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

			var updatedVisit = await _visitService.UpdateVisitDiagnosisAsync(model.Id, model.Diagnosis , physicianId);

			if(updatedVisit is null) return NotFound(new ApiResponse(404, "Visit not found."));

			return Ok(_mapper.Map<DailyVisitToReturnDto>(updatedVisit));
		}
	}
}
