using LetusCountApplication.Domain.Models.EFModels;
using LetusCountApplication.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LetusCountApplication.Infrastructure.Data.Context
{
	public class LetusCountApplicationContext(DbContextOptions<LetusCountApplicationContext> options) : DbContext(options)
	{
		/// <summary>
		/// Departments.
		/// </summary>
		public DbSet<Department> Departments { get; set; }

		/// <summary>
		/// Cashes.
		/// </summary>
		public DbSet<Cash> Cashes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));
			modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
			modelBuilder.ApplyConfiguration(new CashConfiguration());			

			base.OnModelCreating(modelBuilder);
		}
	}
}
