using LetusCountApplication.Application.Interfaces;
using LetusCountApplication.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LetusCountApplication.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		var connectionString =
			configuration.GetConnectionString("LetUsCountDatabase")
			?? Environment.GetEnvironmentVariable("LetUsCountDatabase");

		if (string.IsNullOrWhiteSpace(connectionString))
			throw new InvalidOperationException("Connection string is not configured.");

		services.AddDbContext<LetusCountApplicationContext>(options =>
			options.UseNpgsql(connectionString));

		services.AddScoped<IApplicationDbContext>(sp =>
			sp.GetRequiredService<LetusCountApplicationContext>());

		return services;
	}
}
