using LetusCountService.Application.Interfaces;

namespace LetusCountService.Application.Services
{
	/// <summary>
	/// Send message to email.
	/// </summary>	
	public class SendFailureNotificationHandler(IEmailSender emailSender)
	{
		private readonly IEmailSender _emailSender = emailSender;

		public async Task Handle(string message, CancellationToken ct)
		{
			await _emailSender.SendAsync(message, ct);
		}
	}
}
