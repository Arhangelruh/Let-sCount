using LetusCountApplication.Domain.Constants;
using LetusCountApplication.Domain.Models.EFModels;
using LetusCountApplication.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetusCountApplication.Infrastructure.Data.Configurations
{
	public class CashConfiguration : IEntityTypeConfiguration<Cash>
	{
		/// <summary>
		/// EF Configuration for Cashes.
		/// </summary>
		public void Configure(EntityTypeBuilder<Cash> builder)
		{
			_ = builder ?? throw new ArgumentNullException(nameof(builder));

			builder.ToTable(TableConstants.Cashes, SchemaConstants.Departments)
			   .HasKey(cash => cash.Id);

			builder.Property(cash => cash.Name)
				.IsRequired()
				.HasMaxLength(FieldLengthsConstants.MaxLengthShortMedium);

			builder.HasOne(department => department.Department)
			 .WithMany(cash => cash.Cashes)
			 .HasForeignKey(cash => cash.DepartmentId)
			 .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
