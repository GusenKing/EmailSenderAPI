using EmailSender.Data.Models;

namespace EmailSenderAPI.Services.EmailLogsService;

public interface IEmailLogsService
{
    public Task<IResult> AddLogEntryAsync(
        string subject,
        string message,
        string address,
        string status
    );

    public Task<IResult> GetLogsAsync();
}
