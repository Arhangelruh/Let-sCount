using LetusCountService.Domain.Models;
using LetusCountService.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetusCountService.Infrastructure.Data.Configurations
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
				.HasMaxLength(ConfigurationConstants.SqlMaxLengthLong);
		}
	}
}
