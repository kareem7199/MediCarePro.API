﻿using AutoMapper;
using MediCarePro.BLL.AccountService;
using MediCarePro.BLL.Dtos;
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
		private readonly IAccountService _accountService;
		private readonly IMapper _mapper;

		public ReceptionScreenController(
			ISpecialtyService specialtyService,
			IAccountService accountService,
			IMapper mapper)
        {
			_specialtyService = specialtyService;
			_accountService = accountService;
			_mapper = mapper;
		}

		[HttpGet("Specialty")]
		public async Task<ActionResult<IReadOnlyList<Specialty>>> GetSpecialties()
		{
			var Specialties = await _specialtyService.GetSpecialtiesAsync();

			return Ok(Specialties);
		}

		[HttpGet("Physician")]
		public async Task<ActionResult<IReadOnlyList<Account>>> GetPhysicians([FromQuery]  int SpecialtyId)
		{
			var physicians = await _accountService.GetPhysiciansAsync(SpecialtyId);

			return Ok(_mapper.Map<IReadOnlyList<PhysicianDto>>(physicians));
		}

	}
}
