using Microsoft.EntityFrameworkCore;

namespace LetusCountService.Infrastructure.Data.Context
{
	public class LetusCountServiceContext(DbContextOptions<LetusCountServiceContext> options) : DbContext(options)
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

			base.OnModelCreating(modelBuilder);
		}
	}
}
