using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCarePro.DAL.Data.Config
{
	internal class DiagnosisConfigurations : IEntityTypeConfiguration<Diagnosis>
	{
		public void Configure(EntityTypeBuilder<Diagnosis> builder)
		{
			builder.Property(D => D.Fees)
				   .HasColumnType("decimal(18,2)");

			builder
				.HasIndex(D => new { D.VisitId, D.BoneName })
				.IsUnique();
		}
	}
}
