using MediCarePro.API.Errors;
using MediCarePro.BLL.AuthService;
using MediCarePro.BLL.Dtos;
using MediCarePro.DAL.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MediCarePro.API.Controllers
{
	public class LoginController : BaseApiController
	{
		private readonly UserManager<Account> _userManager;
		private readonly SignInManager<Account> _signInManager;
		private readonly IAuthService _authService;

		//"Physician", "Reception"

		public LoginController(
			UserManager<Account> userManager,
			SignInManager<Account> signInManager,
			IAuthService authService)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_authService = authService;
		}

		[HttpPost]
		public async Task<ActionResult<AccountDto>> Login(LoginDto model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user == null)
				return Unauthorized(new ApiResponse(401, "Invalid login"));

			var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

			if (!result.Succeeded)
				return Unauthorized(new ApiResponse(401, "Invalid login"));

			var roles = await _userManager.GetRolesAsync(user);

			return Ok(new AccountDto()
			{
				Email = user.Email,
				Role = roles.FirstOrDefault(), // Assuming a user has only one role for simplicity
				Token = await _authService.CreateTokenAsync(user)
			});
		}
	}
}
