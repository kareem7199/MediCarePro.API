using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace MediCarePro.DAL.Data
{
	public static class StoreContextSeeder
	{
		private static string[] roles = { "Physician", "Reception" };

		public static async Task SeedUsersAsync(UserManager<Account> userManager , StoreContext storeContext)
		{

			if (!userManager.Users.Any())
			{

				var specialty = new Specialty()
				{
					Name = "Test"
				};

				await storeContext.AddAsync(specialty);

				await storeContext.SaveChangesAsync();

				var physician = new Account()
				{
					FirstName = "Kareem",
					SecondName = "Tamer",
					Email = "kareemtameregy@gmail.com",
					UserName = "kareem.tamer",
					PhoneNumber = "01025578893",
					SpecialtyId = specialty.Id
				};


				await userManager.CreateAsync(physician, "Pa$$w0rd");
				await userManager.AddToRoleAsync(physician, roles[0]);

				var schedule = new PhysicianSchedule()
				{
					AccountId = physician.Id,
					Day = Day.Saturday,
					StartTime = new TimeOnly(14, 30),
					EndTime = new TimeOnly(20 , 0)
				};

				await storeContext.AddAsync(schedule);

				var reception = new Account()
				{
					FirstName = "Kareem",
					SecondName = "Tamer",
					Email = "kareemtameregy+1@gmail.com",
					UserName = "kareem.tamer1",
					PhoneNumber = "01025578893"
				};

				await userManager.CreateAsync(reception, "Pa$$w0rd");
				await userManager.AddToRoleAsync(reception, roles[1]);

				var patient = new Patient()
				{
					Name = "Kareem Tamer" ,
					Age = 21 ,
					PhoneNumber = "01025578893"
				};

				await storeContext.AddAsync(patient);

				await storeContext.SaveChangesAsync();

			}
		}

		public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
		{

			foreach (var roleName in roles)
			{
				if (!await roleManager.RoleExistsAsync(roleName))
				{
					await roleManager.CreateAsync(new IdentityRole(roleName));
				}
			}
		}
	}
}
