using LetusCountApplication.Application.Interfaces;
using LetusCountApplication.Domain.Models.EFModels;
using LetusCountApplication.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LetusCountApplication.Infrastructure.Data.Context
{
	public class LetusCountApplicationContext(DbContextOptions<LetusCountApplicationContext> options) : DbContext(options), IApplicationDbContext
	{
		/// <summary>
		/// Departments.
		/// </summary>
		public DbSet<Department> Departments { get; set; } = null!;

		/// <summary>
		/// Cashes.
		/// </summary>
		public DbSet<Cash> Cashes { get; set; } = null!;

		/// <summary>
		/// Cash machines.
		/// </summary>
		public DbSet<CashMachine> CashMachines { get; set; } = null!;

		/// <summary>
		/// Connecting table for cashes and cashmachines.
		/// </summary>
		public DbSet<CashCashMachine> CashCashMachines { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));
			modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
			modelBuilder.ApplyConfiguration(new CashConfiguration());
			modelBuilder.ApplyConfiguration(new CashMachineConfiguration());
			modelBuilder.ApplyConfiguration(new CashCashMachineConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
