﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCarePro.DAL.Data.Config
{
	public class VisitConfigurations : IEntityTypeConfiguration<Visit>
	{
		public void Configure(EntityTypeBuilder<Visit> builder)
		{

			builder.HasOne(V => V.PhysicianSchedule)
				   .WithMany()
				   .HasForeignKey(V => V.PhysicianScheduleId)
				   .OnDelete(DeleteBehavior.NoAction);

			builder.HasIndex(V => new { V.Date, V.PhysicianScheduleId })
				   .IsUnique();

			builder.Property(V => V.PhysicanFees)
				   .HasColumnType("decimal(18,2)");

			builder.Property(V => V.Date)
				   .HasConversion(
					v => v.ToDateTime(TimeOnly.MinValue),
					v => DateOnly.FromDateTime(v));      


		}
	}
}