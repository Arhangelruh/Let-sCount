using LetusCountApplication.Domain.Models.EFModels;
using LetusCountApplication.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetusCountApplication.Infrastructure.Data.Configurations
{
	public class CashCashMashineConfiguration : IEntityTypeConfiguration<CashCashMashine>
	{
		/// <summary>
		/// EF Configuration for CashCashMashines.
		/// </summary>
		public void Configure(EntityTypeBuilder<CashCashMashine> builder)
		{
			_ = builder ?? throw new ArgumentNullException(nameof(builder));

			builder.ToTable(TableConstants.CashCashMashines, SchemaConstants.Departments)
			   .HasKey(c => c.Id);

			builder.HasOne(cash => cash.Cash)
			.WithMany(ccm => ccm.CashCashMashines)
			.HasForeignKey(cash => cash.CashId)
			.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(cm => cm.CashMachine)
			.WithMany(ccm => ccm.CashCashMashine)
			.HasForeignKey(cm => cm.CashMashineId)
			.OnDelete(DeleteBehavior.Restrict);

			builder.Property(cm => cm.StartWorking)
				.HasColumnType("timestamp with time zone");

			builder.Property(cm => cm.EndWorking)
				.HasColumnType("timestamp with time zone");
		}
	}
}
