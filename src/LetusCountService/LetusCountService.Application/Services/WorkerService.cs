using LetusCountService.Application.Exceptions;
using LetusCountService.Application.Interfaces;
using LetusCountService.Domain.Models;

namespace LetusCountService.Application.Services
{
	///<inheritdoc/>
	public class WorkerService(IXmlParser<Operation> xmlParser, ISaveOperationService saveOperationService, IFileWorker fileWorker) : IWorkerService
	{
		private readonly IXmlParser<Operation> _xmlParser = xmlParser ?? throw new ArgumentNullException(nameof(xmlParser));
		private readonly ISaveOperationService _saveOperationService = saveOperationService ?? throw new ArgumentNullException(nameof(saveOperationService));
		private readonly IFileWorker _fileWorker = fileWorker ?? throw new ArgumentNullException(nameof(fileWorker));

		public async Task WorkAsync(string filePath, CancellationToken ct)
		{
			ArgumentNullException.ThrowIfNull(filePath);
			try
			{
				var operation = await ParseAsync(filePath, ct);
			
				FileInfo fileInfo = new(filePath);
				DateTime createData = fileInfo.CreationTime;

				operation.StartTime = createData;
				operation.EndTime = createData;
				
				await _saveOperationService.SaveOperationAsync(operation);

				await _fileWorker.MoveFile(filePath);
				
			}
			catch (DatabaseNotConfiguredException ex)
			{
				throw new FilePersistenceException(filePath, ex);
			}
			catch (FormatException ex)
			{
				throw new InvalidFileFormatException(filePath, ex);
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidFileFormatException(filePath, ex);
			}
			catch (IOException ex)
			{
				throw new FileAccessException(filePath, ex);
			}
			catch (PersistenceException ex)
			{
				throw new FilePersistenceException(filePath, ex);
			}
			catch (Exception ex)
			{
				throw new UnexpectedApplicationException(
				$"Unexpected exception while {filePath} processing.", ex);
			}
		}

		private async Task<Operation> ParseAsync(string filePath, CancellationToken ct)
		{

			const int maxAttempts = 5;
			var delay = TimeSpan.FromSeconds(5);

			for (int attempt = 1; ; attempt++)
			{
				try
				{
					await using var fs = File.OpenRead(filePath);

					return await _xmlParser.ParseAsync(fs, ct);
				}
				catch when (attempt < maxAttempts)
				{
					await Task.Delay(delay, ct);
				}
			}
		}
	}
}
