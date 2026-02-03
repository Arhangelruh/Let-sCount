using LetusCountApplication.Application.Interfaces;
using LetusCountApplication.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LetusCountApplication.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddScoped<ICashMachinesService, CashMachinesService>();
		services.AddScoped<ICashesService, CashesService>();
		services.AddScoped<IDepartmentsService, DepartmentsService>();
		return services;
	}
}
