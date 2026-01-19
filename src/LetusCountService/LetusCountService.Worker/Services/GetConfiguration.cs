using LetusCountService.Models;

namespace LetusCountService.Services
{
	public class GetConfiguration
	{
		/// <summary>
		/// FTP server address.
		/// </summary>
		public string FolderPath { get; private set; }

		/// <summary>
		/// Amount of threads.
		/// </summary>
		public int WorkerCount { get; private set; }

		public GetConfiguration(FolderSettings folderSettings, ThreadsSettings threadsSettings)
		{
			FolderPath = folderSettings.FolderPath ?? throw new ArgumentNullException(nameof(folderSettings.FolderPath));

			if (int.TryParse(threadsSettings.Threads, out int number))
			{
				WorkerCount = number;
			}
			else
			{
				WorkerCount = 1;
			}
		}
	}
}
