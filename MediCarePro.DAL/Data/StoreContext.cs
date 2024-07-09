using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MediCarePro.DAL.Data
{
	public class StoreContext : IdentityDbContext
	{
		public StoreContext(DbContextOptions<StoreContext> options)
			: base(options)
		{

		}
        public DbSet<Specialty> Specialties { get; set; }
    }
}
