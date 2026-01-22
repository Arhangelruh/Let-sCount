using LetusCountApplication.Domain.Constants;
using LetusCountApplication.Domain.Models.EFModels;
using LetusCountApplication.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetusCountApplication.Infrastructure.Data.Configurations
{
	/// <summary>
	/// EF Configuration for Departments.
	/// </summary>
	public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			_ = builder ?? throw new ArgumentNullException(nameof(builder));

			builder.ToTable(TableConstants.Departments, SchemaConstants.Departments)
			   .HasKey(department => department.Id);

			builder.Property(department => department.Name)
				.IsRequired()
				.HasMaxLength(FieldLengthsConstants.MaxLengthShortMedium);

			builder.Property(department => department.Address)
				.IsRequired()
				.HasMaxLength(FieldLengthsConstants.MaxLengthLongMedium);
		}
	}
}
