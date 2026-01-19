namespace LetusCountService.Application.Exceptions
{
	/// <summary>
	/// File processing esception.
	/// </summary>
	/// <param name="filePath">Path to file</param>
	/// <param name="message">Message</param>
	/// <param name="innerException">Exception</param>
	public abstract class FileProcessingException(
		string filePath,
		string message,
		Exception? innerException = null)
				: ApplicationExceptionBase(message, innerException)
	{
		public string FilePath { get; } = filePath;
	}
}
