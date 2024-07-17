using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MediCarePro.DAL.Data
{
	public static class StoreContextSeeder
	{
		private static string[] roles = { "Physician", "Reception" };

		public static async Task SeedAsync(UserManager<Account> userManager, StoreContext storeContext)
		{

			if (!storeContext.Specialties.Any())
			{
				var SpecialtiesData = File.ReadAllText("../MediCarePro.DAL/Data/DataSeed/specialties.json");
				var Specialties = JsonSerializer.Deserialize<List<Specialty>>(SpecialtiesData);

				if (Specialties?.Count > 0)
				{
					foreach (var Specialty in Specialties)
					{
						await storeContext.AddAsync(Specialty);
					}
					await storeContext.SaveChangesAsync();
				}

				if (!userManager.Users.Any())
				{
					var i = 0;
					foreach (var Specialty in Specialties)
					{
						var physician = new Account()
						{
							FirstName = "Kareem",
							SecondName = "Tamer",
							Email = $"kareemtameregy{i}@gmail.com",
							UserName = $"kareem.tamer{i}",
							PhoneNumber = "01025578893",
							SpecialtyId = Specialty.Id
						};

						await userManager.CreateAsync(physician, "Pa$$w0rd");
						await userManager.AddToRoleAsync(physician, roles[0]);

						var PhysicianSchedule = new PhysicianSchedule()
						{
							AccountId = physician.Id,
							StartTime = new TimeOnly(13, 00),
							EndTime = new TimeOnly(18, 00),
							Day = Day.Saturday
						};

						var PhysicianSchedule2 = new PhysicianSchedule()
						{
							AccountId = physician.Id,
							StartTime = new TimeOnly(9, 00),
							EndTime = new TimeOnly(17, 00),
							Day = Day.Monday
						};

						var PhysicianSchedule3 = new PhysicianSchedule()
						{
							AccountId = physician.Id,
							StartTime = new TimeOnly(15, 00),
							EndTime = new TimeOnly(11, 00),
							Day = Day.Thursday
						};

						await storeContext.AddAsync(PhysicianSchedule);
						await storeContext.AddAsync(PhysicianSchedule2);
						await storeContext.AddAsync(PhysicianSchedule3);

						await storeContext.SaveChangesAsync();

						i++;
					}

					var reception = new Account()
					{
						FirstName = "Kareem",
						SecondName = "Tamer",
						Email = $"kareemtameregy+{i}@gmail.com",
						UserName = $"kareem.tamer{i}",
						PhoneNumber = "01025578893"
					};

					await userManager.CreateAsync(reception, "Pa$$w0rd");
					await userManager.AddToRoleAsync(reception, roles[1]);
				}
			}
			if (!storeContext.Patients.Any())
			{
				var PatientsData = File.ReadAllText("../MediCarePro.DAL/Data/DataSeed/patients.json");
				var Patients = JsonSerializer.Deserialize<List<Patient>>(PatientsData);

				if (Patients?.Count > 0)
				{
					foreach (var Patient in Patients)
					{
						await storeContext.AddAsync(Patient);
					}
					await storeContext.SaveChangesAsync();
				}
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
