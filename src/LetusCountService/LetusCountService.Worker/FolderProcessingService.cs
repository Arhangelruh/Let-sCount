using LetusCountService.Application.Exceptions;
using LetusCountService.Application.Interfaces;
using LetusCountService.Application.Services;
using LetusCountService.Services;
using System.Threading.Channels;

namespace LetusCountService
{
	public class FolderProcessingService : BackgroundService
	{
		private readonly ILogger<FolderProcessingService> _logger;
		private readonly Channel<string> _queue;
		private FileSystemWatcher? _watcher;
		private readonly GetConfiguration _configuration;
		private readonly IServiceScopeFactory _scopeFactory;

		public FolderProcessingService(
			ILogger<FolderProcessingService> logger,
			GetConfiguration getConfiguration,
			IServiceScopeFactory scopeFactory
			)
		{
			_logger = logger;
			_configuration = getConfiguration ?? throw new ArgumentNullException(nameof(getConfiguration));

			_queue = Channel.CreateUnbounded<string>(new UnboundedChannelOptions
			{
				SingleReader = false,
				SingleWriter = false
			});
			_scopeFactory = scopeFactory;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			if (Directory.Exists(_configuration.FolderPath))
			{
				ScanExistingFiles(_configuration.FolderPath);

				StartWatcher(_configuration.FolderPath);

				_logger.LogInformation("LetUsCountervice started");


				var workers = Enumerable.Range(0, _configuration.WorkerCount)
		        .Select(_ => Task.Run(() => ConsumeAsync(stoppingToken)));

				await Task.WhenAll(workers);
			}
			else
			{
				_logger.LogError($"Folder not found {_configuration.FolderPath}");
				await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
			}
		}

		private void ScanExistingFiles(string path)
		{
			_logger.LogInformation("Scanning existing files in {Path}", path);

			foreach (var file in Directory.GetFiles(path, "*.xml"))
			{
				Enqueue(file);
			}
		}

		private void StartWatcher(string path)
		{
			_watcher = new FileSystemWatcher(path)
			{
				Filter = "*.xml",
				NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size,
				EnableRaisingEvents = true
			};

			_watcher.Created += (_, e) => Enqueue(e.FullPath);
			_watcher.Renamed += (_, e) => Enqueue(e.FullPath);
		}

		private void Enqueue(string file)
		{
			_logger.LogInformation("File detected: {File}", file);
			_queue.Writer.TryWrite(file);
		}

		private async Task WaitUntilFileIsReadyAsync(string file, CancellationToken ct)
		{
			const int retryDelayMs = 500;
			const int maxRetries = 20;

			for (int i = 0; i < maxRetries; i++)
			{
				ct.ThrowIfCancellationRequested();

				try
				{
					using var stream = File.Open(
						file,
						FileMode.Open,
						FileAccess.Read,
						FileShare.None);

					if (stream.Length > 0)
						return;
				}
				catch (IOException)
				{
					// still waiting
				}

				await Task.Delay(retryDelayMs, ct);
			}

			throw new IOException($"File {file} is locked too long");
		}

		private async Task ProcessFileAsync(string file, CancellationToken ct)
		{
			_logger.LogInformation("Processing file {File}", file);

			var scope = _scopeFactory.CreateScope();

			var worker = scope.ServiceProvider
					.GetRequiredService<IWorkerService>();

			try
			{
				await worker.WorkAsync(file, ct);
			}
			catch (FileAccessException)
			{
				_logger.LogWarning($"Error with access to file {file}");
				await SendMessageAsync($"Error with access to file {file}", ct);
			}
			catch (InvalidFileFormatException)
			{
				_logger.LogWarning($"Error: invalid file {file} format");
				await SendMessageAsync($"Error: invalid file {file} format", ct);
			}
			catch (FileProcessingException)
			{
				_logger.LogWarning($"Error: can't save data to database while file: {file} processing");
				await SendMessageAsync($"Error: can't save data to database while file: {file} processing", ct);
			}
			catch (UnexpectedApplicationException)
			{
				_logger.LogWarning($"Error: unexpected exception while file: {file} processing");
				await SendMessageAsync($"Error: unexpected exception while file: {file} processing", ct);
			}
			//await Task.Delay(1000, ct);

			_logger.LogInformation("File processed {File}", file);
		}

		public override Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Stopping FolderProcessingService");

			_watcher?.Dispose();
			_queue.Writer.TryComplete();

			return base.StopAsync(cancellationToken);
		}

		private async Task SendMessageAsync(string message, CancellationToken cancellationToken)
		{

			try
			{
				var scope = _scopeFactory.CreateScope();

				var sender = scope.ServiceProvider
						.GetRequiredService<SendFailureNotificationHandler>();

				await sender.Handle(message, cancellationToken);
			}
			catch (Exception ex)
			{

				_logger.LogWarning($"Error sending email: {ex.Message}");
			}
		}

		private async Task ConsumeAsync(CancellationToken ct)
		{
			await foreach (var file in _queue.Reader.ReadAllAsync(ct))
			{
				try
				{
					await WaitUntilFileIsReadyAsync(file, ct);
					await ProcessFileAsync(file, ct);
				}
				catch (OperationCanceledException)
				{
					// correct ending.
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error processing {File}", file);
				}
			}
		}
	}
}