using LetusCountApplication.Domain.Models.EFModels;
using LetusCountApplication.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetusCountApplication.Infrastructure.Data.Configurations
{
	public class CashCashMachineConfiguration : IEntityTypeConfiguration<CashCashMachine>
	{
		/// <summary>
		/// EF Configuration for CashCashMachines.
		/// </summary>
		public void Configure(EntityTypeBuilder<CashCashMachine> builder)
		{
			_ = builder ?? throw new ArgumentNullException(nameof(builder));

			builder.ToTable(TableConstants.CashCashMachines, SchemaConstants.Departments)
			   .HasKey(c => c.Id);

			builder.HasOne(cash => cash.Cash)
			.WithMany(ccm => ccm.CashCashMachines)
			.HasForeignKey(cash => cash.CashId)
			.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(cm => cm.CashMachine)
			.WithMany(ccm => ccm.CashCashMachine)
			.HasForeignKey(cm => cm.CashMachineId)
			.OnDelete(DeleteBehavior.Restrict);

			builder.Property(cm => cm.StartWorking)
				.HasColumnType("timestamp with time zone");

			builder.Property(cm => cm.EndWorking)
				.HasColumnType("timestamp with time zone");
		}
	}
}
