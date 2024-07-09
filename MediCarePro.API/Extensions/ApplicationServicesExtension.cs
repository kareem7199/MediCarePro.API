using System.Text;
using MediCarePro.API.Errors;
using MediCarePro.API.Middlewares;
using MediCarePro.BLL;
using MediCarePro.BLL.AuthService;
using MediCarePro.BLL.SpecialtyService;
using MediCarePro.DAL.Data;
using MediCarePro.DAL.Data.Entities;
using MediCarePro.DAL.Interfaces;
using MediCarePro.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MediCarePro.API.Extensions
{
	public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{

			services.AddAutoMapper(typeof(MappingProfile));

			services.AddScoped(typeof(IAuthService), typeof(AuthService));
			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
			services.AddScoped(typeof(ISpecialtyService) , typeof(SpecialtyService));

			services.AddScoped<ExceptionMiddleware>();

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{

					var errors = actionContext.ModelState
												   .Where(P => P.Value.Errors.Count > 0)
												   .SelectMany(P => P.Value.Errors)
												   .Select(E => E.ErrorMessage)
												   .ToList();
					var response = new ApiValidationErrorResponse() { Errors = errors };

					return new BadRequestObjectResult(response);
				};
			});

			return services;
		}

		public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddIdentity<Account, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
							.AddEntityFrameworkStores<StoreContext>()
							.AddDefaultTokenProviders();

			services.AddAuthorization(options =>
			{
				options.AddPolicy("Physician", policy => policy.RequireRole("Physician"));
				options.AddPolicy("Reception", policy => policy.RequireRole("Reception"));
			});

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = configuration["JWT:ValidIssuer"],
					ValidateAudience = true,
					ValidAudience = configuration["JWT:ValidAudience"],
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"] ?? string.Empty)),
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero
				};
			});

			return services;
		}
	}
}
