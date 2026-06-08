namespace LetusCountService.Application.Exceptions
{
	/// <summary>
	/// File access exception.
	/// </summary>
	/// <param name="filePath">File path</param>
	/// <param name="innerException">Exception</param>
	public sealed class FileAccessException(
		string filePath,
		Exception innerException)
		: FileProcessingException(
			filePath,
			$"Problem with file {filePath} access",
			innerException)
	{
	}
}
