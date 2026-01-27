using LetusCountApplication.Domain.Constants;
using LetusCountApplication.Domain.Models.EFModels;
using LetusCountApplication.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetusCountApplication.Infrastructure.Data.Configurations
{
	public class CashMachineConfiguration : IEntityTypeConfiguration<CashMachine>
	{
		/// <summary>
		/// EF Configuration for CashMachines.
		/// </summary>
		public void Configure(EntityTypeBuilder<CashMachine> builder)
		{
			_ = builder ?? throw new ArgumentNullException(nameof(builder));

			builder.ToTable(TableConstants.CashMashines, SchemaConstants.Departments)
			   .HasKey(cm => cm.Id);

			builder.Property(cm => cm.Serial)
				.IsRequired()
				.HasMaxLength(FieldLengthsConstants.MaxLengthShort);
		}
	}
}
