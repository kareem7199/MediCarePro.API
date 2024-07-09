using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
	}
}
