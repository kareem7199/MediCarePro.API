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
	public class PhysicianScheduleConfigurations : IEntityTypeConfiguration<PhysicianSchedule>
	{
		public void Configure(EntityTypeBuilder<PhysicianSchedule> builder)
		{
			builder.Property(PS => PS.Day)
				   .HasConversion(
				   (PSDay) => PSDay.ToString(),
				   (PSDay) => (Day)Enum.Parse(typeof(Day), PSDay)
				   );

			builder.Property(PS => PS.StartTime)
			.HasConversion(
				ST => ST.ToTimeSpan(),
				ST => TimeOnly.FromTimeSpan(ST));


			builder.Property(PS => PS.EndTime)
			.HasConversion(
				ET => ET.ToTimeSpan(),
				ET => TimeOnly.FromTimeSpan(ET));

		}
	}
}
