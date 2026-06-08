namespace LetusCountService.Application.Interfaces
{
	public interface IEmailSender
	{
		/// <summary>
		/// Send message to email.
		/// </summary>
		/// <param name="message">Message for sending</param>
		/// <param name="ct">Cancellation token</param>
		/// <returns></returns>
		Task SendAsync(string message, CancellationToken ct);
	}
}
