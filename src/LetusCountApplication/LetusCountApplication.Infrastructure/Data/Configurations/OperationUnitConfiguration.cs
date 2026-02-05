using LetusCountApplication.Domain.Constants;
using LetusCountApplication.Domain.Models;
using LetusCountApplication.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetusCountApplication.Infrastructure.Data.Configurations
{
	/// <summary>
	/// EF Configuration for OperationUnit.
	/// </summary>
	public class OperationUnitConfiguration : IEntityTypeConfiguration<OperationUnit>
	{
		public void Configure(EntityTypeBuilder<OperationUnit> builder)
		{
			_ = builder ?? throw new ArgumentNullException(nameof(builder));

			builder.ToTable(TableConstants.OperationUnits, SchemaConstants.Operations)
				.HasKey(unit => unit.Id);

			builder.Property(un => un.Currency)
				.IsRequired()
				.HasMaxLength(FieldLengthsConstants.MaxLengthShort);

			builder.HasOne(operation => operation.Operation)
			 .WithMany(unit => unit.OperationUnits)
			 .HasForeignKey(operation => operation.OperationId)
			 .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
