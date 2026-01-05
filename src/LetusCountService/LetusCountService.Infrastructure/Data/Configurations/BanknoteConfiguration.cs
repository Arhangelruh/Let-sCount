using LetusCountService.Domain.Models;
using LetusCountService.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetusCountService.Infrastructure.Data.Configurations
{
	/// <summary>
	/// EF Configuration for Banknote.
	/// </summary>
	public class BanknoteConfiguration : IEntityTypeConfiguration<Banknote>
	{
		public void Configure(EntityTypeBuilder<Banknote> builder)
		{
			_ = builder ?? throw new ArgumentNullException(nameof(builder));

			builder.ToTable(TableConstants.Banknotes, SchemaConstants.Operations)
				.HasKey(Banknote => Banknote.Id);

			builder.Property(banknote => banknote.DenomName)
				.IsRequired()
				.HasMaxLength(ConfigurationConstants.SqlMaxLengthShort);

			builder.Property(banknote => banknote.Value)
				.IsRequired()
				.HasMaxLength(ConfigurationConstants.SqlMaxLengthShort);

			builder.Property(banknote => banknote.SerialNumber)
				.IsRequired()
				.HasMaxLength(ConfigurationConstants.SqlMaxLengthShort);

			builder.HasOne(operation => operation.OperationUnit)
				.WithMany(banknote => banknote.Banknotes)
				.HasForeignKey(banknote => banknote.OperationUnitId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
