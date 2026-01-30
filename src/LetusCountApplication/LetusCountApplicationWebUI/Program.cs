using LetusCountApplication.Application;
using LetusCountApplication.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

var logger = LogManager.Setup()
	.LoadConfigurationFromAppSettings()
	.GetCurrentClassLogger();
try
{
	var builder = WebApplication.CreateBuilder(args);

	builder.Logging.ClearProviders();
	builder.Host.UseNLog();
	builder.Services.AddApplication();
	builder.Services.AddInfrastructure(builder.Configuration);
	builder.Services.AddControllersWithViews();

	// Add services to the container.
	builder.Services.AddRazorPages();

	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (!app.Environment.IsDevelopment())
	{
		app.UseExceptionHandler("/Error");
		// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		app.UseHsts();
	}

	app.UseHttpsRedirection();

	app.UseRouting();

	app.UseAuthorization();

	app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

	app.MapStaticAssets();
	app.MapRazorPages()
	   .WithStaticAssets();

	app.Run();
}
catch (Exception ex)
{
	logger.Error(ex, "Application stopped due to exception");
	throw;
}
finally
{ LogManager.Shutdown(); }