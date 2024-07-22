using AutoMapper;
using MediCarePro.API.Errors;
using MediCarePro.BLL.Dtos;
using MediCarePro.BLL.ReceptionScreenService;
using MediCarePro.DAL.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCarePro.API.Controllers
{
	[Authorize(Roles = "UserCreator")]
	public class UserCreationScreenController : BaseApiController
	{
		private readonly UserManager<Account> _userManager;
		private readonly IReceptionScreenService _receptionScreenService;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IMapper _mapper;

		public UserCreationScreenController(
			UserManager<Account> userManager,
			IReceptionScreenService receptionScreenService,
			RoleManager<IdentityRole> roleManager,
			IMapper mapper)
		{
			_userManager = userManager;
			_receptionScreenService = receptionScreenService;
			_roleManager = roleManager;
			_mapper = mapper;
		}

		[HttpPost("CreateUser")]
		public async Task<ActionResult> CreateUser(CreateUserDto model)
		{
			if (model.Roles.Contains("Physician") && !model.SpecialtyId.HasValue) 
				return BadRequest(new ApiResponse(400, "Specialty is required for users with the 'Physician' role."));


			var account = new Account()
			{
				FirstName = model.FirstName,
				SecondName = model.SecondName,
				PhoneNumber = model.PhoneNumber,
				UserName = model.Email.Split('@')[0],
				Email = model.Email,
			};

			if(model.Roles.Contains("Physician")) account.SpecialtyId = model.SpecialtyId.Value;

			var result = await _userManager.CreateAsync(account, $"{model.Email.Split('@')[0]}@Pa$$w0rd");

			if (!result.Succeeded)
				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors.Select((E) => E.Description) });

			foreach (var role in model.Roles)
				 await _userManager.AddToRoleAsync(account, role);

			return Ok(new {Id = account.Id , Username = account.UserName});
		}

		[HttpGet("Specialty")]
		public async Task<ActionResult<IReadOnlyList<Specialty>>> GetSpecialties()
		{
			var Specialties = await _receptionScreenService.GetSpecialtiesAsync();

			return Ok(Specialties);
		}

		[HttpGet("Role")]
		public async Task<ActionResult<IReadOnlyList<RoleDto>>> GetRoles()
		{
			var roles = await _roleManager.Roles.ToListAsync();

			return Ok(_mapper.Map<IReadOnlyList<RoleDto>>(roles));
		}
	}
}
