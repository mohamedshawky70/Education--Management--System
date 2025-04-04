


using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using School.Application.Settings;


namespace School.Infrastructure.Implementation.Authentication;

public class EmailSender : IEmailSender
{
	private readonly EmailSettings _emailSettings;

	public EmailSender(IOptions<EmailSettings> emailSettings)
	{
		_emailSettings = emailSettings.Value;
	}

	public async Task SendEmailAsync(string email, string subject, string htmlMessage)
	{
		var message = new MimeMessage
		{
			Sender = MailboxAddress.Parse(_emailSettings.Email),//من مين
			Subject = subject
		};

		message.To.Add(MailboxAddress.Parse(email));//لمين

		var builder = new BodyBuilder
		{
			HtmlBody = htmlMessage //Template
		};

		message.Body = builder.ToMessageBody();
		using var smtp = new SmtpClient();// المسؤل عن إرسال الإميل

		smtp.Connect(_emailSettings.Host, _emailSettings.port, SecureSocketOptions.StartTls);
		smtp.Authenticate(_emailSettings.Email, _emailSettings.Password);
		await smtp.SendAsync(message);
		smtp.Disconnect(true);
	}
}
