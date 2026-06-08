namespace LetusCountService.Application.Interfaces
{
	public interface IFileWorker
	{
		/// <summary>
		/// Move file to bak + date folder.
		/// </summary>
		/// <param name="filePath">file for moving</param>
		/// <returns></returns>
		Task MoveFile(string filePath);
	}
}
