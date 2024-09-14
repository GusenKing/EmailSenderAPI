using EmailSender.Data.Models;
using EmailSenderAPI.Repositories.EmailLogsRepository;

namespace EmailSenderAPI.Services.EmailLogsService;

public class EmailLogsService(IEmailLogsRepository emailLogsRepository) : IEmailLogsService
{
    public async Task<IResult> AddLogEntryAsync(
        string subject,
        string message,
        string address,
        string status
    )
    {
        var result = await emailLogsRepository.AddLogEntryAsync(
            new Email
            {
                EmailSubject = subject,
                Message = message,
                Address = address,
                SentTime = DateTime.Now,
                Status = status
            }
        );

        return Results.Ok(result);
    }

    public async Task<IResult> GetLogsAsync()
    {
        var result = await emailLogsRepository.GetLogsAsync();
        return Results.Ok(result);
    }
}
