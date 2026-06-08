namespace LetusCountService.Application.Exceptions
{
	/// <summary>
	/// Base exception.
	/// </summary>
	/// <param name="message">Message</param>
	/// <param name="innerException">Exception</param>
	public abstract class ApplicationExceptionBase(
		string message,
		Exception? innerException = null) : Exception(message, innerException)
	{
	}
}
