using LetusCountService.Application.Interfaces;

namespace LetusCountService.Infrastructure.Files
{
	/// <inheritdoc/>
	public class FileWorker : IFileWorker
	{
		public Task MoveFile(string filePath)
		{
			FileInfo fileInfo = new(filePath);

			DateTime now = DateTime.Now;
			var todaysFolder = Path.Combine(fileInfo.Directory.FullName, "bak", now.ToString("yyyyMMdd"));

			if (!Directory.Exists(todaysFolder))
				Directory.CreateDirectory(todaysFolder);

			var destinationFile = Path.Combine(todaysFolder, fileInfo.Name);

			File.Move(filePath, destinationFile, true);
			return Task.CompletedTask;
		}
	}
}
