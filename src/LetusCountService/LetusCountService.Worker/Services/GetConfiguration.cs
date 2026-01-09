namespace LetusCountService.Services
{
	public class GetConfiguration(FolderSettings folderSettings)
	{
		/// <summary>
		/// FTP server address.
		/// </summary>
		public string FolderPath = folderSettings.FolderPath ?? throw new ArgumentNullException(nameof(folderSettings.FolderPath));
	}
}
