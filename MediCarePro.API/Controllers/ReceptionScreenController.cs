using MediCarePro.BLL.SpecialtyService;
using MediCarePro.DAL.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCarePro.API.Controllers
{
	[Authorize(Roles = "Reception")]
	public class ReceptionScreenController : BaseApiController
	{
		private readonly ISpecialtyService _specialtyService;

		public ReceptionScreenController(ISpecialtyService specialtyService)
        {
			_specialtyService = specialtyService;
		}

		[HttpGet("Specialty")]
		public async Task<ActionResult<IReadOnlyList<Specialty>>> GetSpecialties()
		{
			var Specialties = await _specialtyService.GetSpecialtiesAsync();

			return Ok(Specialties);
		}

    }
}
