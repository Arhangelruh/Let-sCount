using InternalPortal.Core.Interfaces;
using InternalPortal.Infrastucture.Data.Repository;
using LetusCountService;
using LetusCountService.Application.Interfaces;
using LetusCountService.Application.Services;
using LetusCountService.Domain.Models;
using LetusCountService.Infrastructure.Data.Context;
using LetusCountService.Infrastructure.EMail;
using LetusCountService.Infrastructure.EMail.Models;
using LetusCountService.Infrastructure.Files;
using LetusCountService.Infrastructure.Xml.Services;
using LetusCountService.Models;
using LetusCountService.Services;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using System.Text;

var logger = LogManager.Setup()
	.LoadConfigurationFromAppSettings()
	.GetCurrentClassLogger();
try
{
	logger.Info("Starting Let'sCount service");

	var builder = Host.CreateApplicationBuilder(args);
	var folder = builder.Configuration.GetSection("FolderSettings").Get<FolderSettings>();
	var threadsSettings = builder.Configuration.GetSection("ThreadsSettings").Get<ThreadsSettings>();
	string connection = builder.Configuration.GetConnectionString("LetUsCountDatabase");
	Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

	builder.Logging.ClearProviders();
	builder.Logging.AddNLog();
	builder.Services.AddSingleton(s => new GetConfiguration(folder, threadsSettings));
	builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

	builder.Services.AddDbContext<LetusCountServiceContext>(options =>
    options.UseNpgsql(connection));

	builder.Services.AddHostedService<FolderProcessingService>();
	builder.Services.AddScoped(typeof(IWorkerService),typeof(WorkerService));
	builder.Services.AddScoped<IXmlParser<Operation>, XmlParser>();
	builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
	builder.Services.AddScoped(typeof(ISaveOperationService), typeof(SaveOperationService));
	builder.Services.AddScoped(typeof(IFileWorker), typeof(FileWorker));
	builder.Services.AddScoped(typeof(IEmailSender), typeof(EmailSender));
	builder.Services.AddTransient<SendFailureNotificationHandler>();

	var host = builder.Build();
	host.Run();
}
catch (Exception ex) {
	logger.Error(ex, "Application stopped due to exception");
	throw;
}
finally
{ LogManager.Shutdown(); }