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
				await using var fs = File.OpenRead(filePath);
				var operation = await _xmlParser.ParseAsync(fs, ct);

				fs.Dispose();

				FileInfo fileInfo = new(filePath);
				DateTime createData = fileInfo.CreationTime;

				operation.StartTime = createData;
				operation.EndTime = createData;

				await _saveOperationService.SaveOperationAsync(operation);

				await _fileWorker.MoveFile(filePath);
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
	}
}
