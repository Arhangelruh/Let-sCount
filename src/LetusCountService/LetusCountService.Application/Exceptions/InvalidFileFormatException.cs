namespace LetusCountService.Application.Exceptions
{
	/// <summary>
	/// File format exception.
	/// </summary>
	/// <param name="filePath">Path to file</param>
	/// <param name="innerException">Exception</param>
	public sealed class InvalidFileFormatException(
		string filePath,
		Exception innerException)
		: FileProcessingException(
			filePath,
			$"Incorrect file {filePath} format",
			innerException)
	{
	}
}
