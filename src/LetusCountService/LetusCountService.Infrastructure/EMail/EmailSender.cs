using LetusCountService.Application.Interfaces;
using LetusCountService.Infrastructure.EMail.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace LetusCountService.Infrastructure.EMail
{
	/// <inheritdoc/>	
	public class EmailSender(IOptions<MailSettings> options) : IEmailSender
	{
		private readonly MailSettings _options = options.Value;

		public async Task SendAsync(string message, CancellationToken ct)
		{
			if (_options.SMTPServer != null && _options.SMTPPort != null && _options.Sender != null)
			{
				MailAddress from = new(_options.Sender);
				using SmtpClient smtp = new(_options.SMTPServer);

				smtp.Port = int.Parse(_options.SMTPPort);

				if (_options.SMTPLogin != null && _options.SMTPPassword != null)
				{

					smtp.Credentials = new NetworkCredential(_options.SMTPLogin, _options.SMTPPassword);
				}

				if (_options.Receivers != null)
				{
					MailAddress to = new(_options.Receivers[0]);
					using MailMessage mess = new(from, to);

					if (_options.Receivers.Count > 1)
					{
						foreach (var email in _options.Receivers)
						{
							mess.CC.Add(email);
						}
					}

					mess.Subject = "Let's count Service";
					mess.Body = message;

					await smtp.SendMailAsync(mess, ct);
				}
			}
		}
	}
}
