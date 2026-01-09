using LetusCountService;
using LetusCountService.Services;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;

var logger = LogManager.Setup()
	.LoadConfigurationFromAppSettings()
	.GetCurrentClassLogger();
try
{
	logger.Info("Starting Let'sCount service");

	var builder = Host.CreateApplicationBuilder(args);
	var folder = builder.Configuration.GetSection("FolderSettings").Get<FolderSettings>();

	builder.Logging.ClearProviders();
	builder.Logging.AddNLog();
	builder.Services.AddSingleton(s => new GetConfiguration(folder));

	builder.Services.AddHostedService<FolderProcessingService>();

	var host = builder.Build();
	host.Run();
}
catch (Exception ex) {
	logger.Error(ex, "Application stopped due to exception");
	throw;
}
finally
{ LogManager.Shutdown(); }