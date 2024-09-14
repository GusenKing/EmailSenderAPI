using System.Text;
using EmailSenderAPI.Services.EmailLogsService;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailSenderAPI.Services.EmailSenderService;

public class EmailSenderService(IConfiguration configuration, IEmailLogsService emailLogsService)
    : IEmailSenderService
{
    public async Task<IResult> SendEmailMessagesAsync(
        string subject,
        string message,
        List<string> addresses
    )
    {
        var sb = new StringBuilder();
        foreach (var address in addresses)
        {
            var smtpResult = await SendEmailMessage(subject, message, address);
            await emailLogsService.AddLogEntryAsync(subject, message, address, smtpResult);

            if (!smtpResult[0].Equals('2'))
                sb.Append(
                    $"Got this error code while sending email message to {addresses}: \n{smtpResult}\n"
                );
        }

        return sb.Length == 0 ? Results.Ok() : Results.Conflict(sb.ToString());
    }

    private async Task<string> SendEmailMessage(string subject, string message, string email)
    {
        using var emailMessage = new MimeMessage();

        emailMessage.From.Add(
            new MailboxAddress(
                configuration["MailSettings:SenderName"],
                configuration["MailSettings:Address"]
            )
        );
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };

        using var client = new SmtpClient();

        await client.ConnectAsync(
            configuration["MailSettings:Host"],
            int.Parse(configuration["MailSettings:Port"]!),
            true
        );
        await client.AuthenticateAsync(
            configuration["MailSettings:Address"],
            configuration["MailSettings:ApplicationPassword"]
        );
        try
        {
            var result = await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);

            return result;
        }
        catch (Exception ex)
        {
            await client.DisconnectAsync(true);
            return ex.Message;
        }
    }
}
