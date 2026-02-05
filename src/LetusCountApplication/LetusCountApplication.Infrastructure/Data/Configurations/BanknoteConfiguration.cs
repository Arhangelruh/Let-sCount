using LetusCountApplication.Domain.Constants;
using LetusCountApplication.Domain.Models;
using LetusCountApplication.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetusCountApplication.Infrastructure.Data.Configurations
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
				.HasMaxLength(FieldLengthsConstants.MaxLengthShort);

			builder.Property(banknote => banknote.Value)
				.IsRequired()
				.HasMaxLength(FieldLengthsConstants.MaxLengthShort);

			builder.Property(banknote => banknote.SerialNumber)
				.IsRequired()
				.HasMaxLength(FieldLengthsConstants.MaxLengthShort);

			builder.HasOne(operation => operation.OperationUnit)
				.WithMany(banknote => banknote.Banknotes)
				.HasForeignKey(banknote => banknote.OperationUnitId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
