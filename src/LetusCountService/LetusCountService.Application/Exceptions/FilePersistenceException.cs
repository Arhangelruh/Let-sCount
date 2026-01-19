namespace LetusCountService.Application.Exceptions
{
	/// <summary>
	/// File saving to database exception.
	/// </summary>
	/// <param name="filePath">File path</param>
	/// <param name="innerException">Exception</param>
	public sealed class FilePersistenceException(
		string filePath,
		Exception innerException)
		: FileProcessingException(
			filePath,
			"Problem with saving data to database.",
			innerException)
	{
	}
}
