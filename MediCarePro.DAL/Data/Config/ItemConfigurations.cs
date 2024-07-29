using MediCarePro.DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCarePro.DAL.Data.Config
{
	internal class ItemConfigurations : IEntityTypeConfiguration<Item>
	{
		public void Configure(EntityTypeBuilder<Item> builder)
		{
			builder.HasIndex(I => I.Name)
				   .IsUnique();
		}
	}
}
