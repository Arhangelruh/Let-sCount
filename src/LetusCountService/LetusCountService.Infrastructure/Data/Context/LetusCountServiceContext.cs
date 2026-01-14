using LetusCountService.Domain.Models;
using LetusCountService.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LetusCountService.Infrastructure.Data.Context
{
	public class LetusCountServiceContext(DbContextOptions<LetusCountServiceContext> options) : DbContext(options)
	{
		/// <summary>
		/// Operations.
		/// </summary>
		public DbSet<Operation> Operations { get; set; }

		/// <summary>
		/// Operation units.
		/// </summary>
		public DbSet<OperationUnit> OperationUnits { get; set; }

		/// <summary>
		/// Banknotes.
		/// </summary>
		public DbSet<Banknote> Banknotes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));
			modelBuilder.ApplyConfiguration(new OperationConfiguration());
			modelBuilder.ApplyConfiguration(new OperationUnitConfiguration());
			modelBuilder.ApplyConfiguration(new BanknoteConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
