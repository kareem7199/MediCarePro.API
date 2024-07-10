using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MediCarePro.DAL.Data
{
	public class StoreContext : IdentityDbContext<Account>
	{
		public StoreContext(DbContextOptions<StoreContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}

        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<PhysicianSchedule> PhysicianSchedules { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visits { get; set; }
    }
}
