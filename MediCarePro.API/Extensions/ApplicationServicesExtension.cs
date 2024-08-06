﻿using System.Text;
using MediCarePro.API.Errors;
using MediCarePro.API.Middlewares;
using MediCarePro.BLL;
using MediCarePro.BLL.Services.AccountService;
using MediCarePro.BLL.Services.AuthService;
using MediCarePro.BLL.Services.InventoryService;
using MediCarePro.BLL.Services.ItemCreationScreenService;
using MediCarePro.BLL.Services.RabbitMqService;
using MediCarePro.BLL.Services.ReceptionScreenService;
using MediCarePro.BLL.Services.TransactionCreationScreenService;
using MediCarePro.BLL.Services.VisitService;
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
			services.AddScoped(typeof(IAccountRepository), typeof(AccountRepository));
			services.AddScoped(typeof(IReceptionScreenService), typeof(ReceptionScreenService));
			services.AddScoped(typeof(IAccountService), typeof(AccountService));
			services.AddScoped(typeof(IVisitService), typeof(VisitService));
			services.AddScoped(typeof(IItemCreationScreenService), typeof(ItemCreationScreenService));
			services.AddScoped(typeof(ITransactionCreationScreenService), typeof(TransactionCreationScreenService));
			services.AddScoped(typeof(IInventoryService), typeof(InventoryService));
			services.AddScoped(typeof(IRabbitMqService), typeof(RabbitMqService));
			services.AddScoped(typeof(IPatientRepository), typeof(PatientRepository));

			services.AddSingleton<ElasticSearchClient>();

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
				options.AddPolicy("UserCreator", policy => policy.RequireRole("UserCreator"));
				options.AddPolicy("ItemCreator", policy => policy.RequireRole("ItemCreator"));
				options.AddPolicy("TransactionCreator", policy => policy.RequireRole("TransactionCreator"));
				options.AddPolicy("InventoryManager", policy => policy.RequireRole("InventoryManager"));
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

				options.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						var accessToken = context.Request.Query["access_token"];

						// If the request is for the hub...
						var path = context.HttpContext.Request.Path;
						if (!string.IsNullOrEmpty(accessToken) &&
							(path.StartsWithSegments("/visithub")))
						{
							// Read the token from the query string
							context.Token = accessToken;
						}
						return Task.CompletedTask;
					}
				};
			});

			return services;
		}
	}
}
