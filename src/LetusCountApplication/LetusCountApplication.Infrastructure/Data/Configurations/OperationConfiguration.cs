using LetusCountApplication.Domain.Constants;
using LetusCountApplication.Domain.Models;
using LetusCountApplication.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetusCountApplication.Infrastructure.Data.Configurations
{
	/// <summary>
	/// EF Configuration for Operation.
	/// </summary>
	public class OperationConfiguration : IEntityTypeConfiguration<Operation>
	{
		public void Configure(EntityTypeBuilder<Operation> builder)
		{
			_ = builder ?? throw new ArgumentNullException(nameof(builder));

			builder.ToTable(TableConstants.Operations, SchemaConstants.Operations)
			   .HasKey(operation => operation.Id);

			builder.Property(operation => operation.MachineSerial)
				.IsRequired()
				.HasMaxLength(FieldLengthsConstants.MaxLengthLongMedium);

			builder.Property(operation => operation.StartTime)
				.HasColumnType("timestamp with time zone");

			builder.Property(operation => operation.EndTime)
				.HasColumnType("timestamp with time zone");
		}
	}
}
