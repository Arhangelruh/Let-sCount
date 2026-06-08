namespace LetusCountService.Application.Exceptions
{
	/// <summary>
	/// Unexpected exception.
	/// </summary>
	/// <param name="message">Message</param>
	/// <param name="innerException">Exception</param>
	public sealed class UnexpectedApplicationException(
		string message,
		Exception innerException)
		: ApplicationExceptionBase(message, innerException)
	{
	}
}
