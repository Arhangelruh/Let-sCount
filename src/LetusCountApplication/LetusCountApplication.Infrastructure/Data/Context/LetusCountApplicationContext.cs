using LetusCountApplication.Application.Interfaces;
using LetusCountApplication.Domain.Models;
using LetusCountApplication.Infrastructure.Data.Configurations;
using LetusCountApplication.Infrastructure.Data.Constants;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LetusCountApplication.Infrastructure.Data.Context
{
	public class LetusCountApplicationContext(DbContextOptions<LetusCountApplicationContext> options) : IdentityDbContext<User>(options), IApplicationDbContext
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

		/// <summary>
		/// Operations.
		/// </summary>
		public DbSet<Operation> Operations { get; set; } = null!;

		/// <summary>
		/// Operation units.
		/// </summary>
		public DbSet<OperationUnit> OperationUnits { get; set; } = null!;

		/// <summary>
		/// Banknotes.
		/// </summary>
		public DbSet<Banknote> Banknotes { get; set; } = null!;

		/// <summary>
		/// Profiles.
		/// </summary>
		public DbSet<Profile> Profiles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));
			modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
			modelBuilder.ApplyConfiguration(new CashConfiguration());
			modelBuilder.ApplyConfiguration(new CashMachineConfiguration());
			modelBuilder.ApplyConfiguration(new CashCashMachineConfiguration());
			modelBuilder.ApplyConfiguration(new ProfileConfiguration());

			modelBuilder.ApplyConfiguration(new OperationConfiguration());
			modelBuilder.Entity<Operation>()
			 .ToTable(TableConstants.Operations, SchemaConstants.Operations, t => t.ExcludeFromMigrations());

			modelBuilder.ApplyConfiguration(new OperationUnitConfiguration());
			modelBuilder.Entity<OperationUnit>()
			 .ToTable(TableConstants.OperationUnits, SchemaConstants.Operations, t => t.ExcludeFromMigrations());

			modelBuilder.ApplyConfiguration(new BanknoteConfiguration());
			modelBuilder.Entity<Banknote>()
			.ToTable(TableConstants.Banknotes, SchemaConstants.Operations, t => t.ExcludeFromMigrations());

			base.OnModelCreating(modelBuilder);
		}
	}
}
