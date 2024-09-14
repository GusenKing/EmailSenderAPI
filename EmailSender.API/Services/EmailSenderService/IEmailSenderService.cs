namespace EmailSenderAPI.Services.EmailSenderService;

public interface IEmailSenderService
{
    public Task<IResult> SendEmailMessagesAsync(
        string subject,
        string message,
        List<string> addresses
    );
}
