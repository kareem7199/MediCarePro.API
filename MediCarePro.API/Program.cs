
using MediCarePro.API.Extensions;
using MediCarePro.API.Middlewares;
using MediCarePro.DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MediCarePro.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddApplicationServices();
			builder.Services.AddAuthServices(builder.Configuration);

			builder.Services.AddControllers().AddNewtonsoftJson(options =>
			{
				options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			});

			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("MyPolicy", policyOptions =>
				{
					policyOptions.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
				});
			});

			var app = builder.Build();

			using var scope = app.Services.CreateScope();

			var services = scope.ServiceProvider;


			var _dbContext = services.GetRequiredService<StoreContext>(); // ask clr for creating object from DbContext Explicitly

			var loggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				await _dbContext.Database.MigrateAsync();
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an error has been occured during apply the migration");
			}

			app.UseMiddleware<ExceptionMiddleware>();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
