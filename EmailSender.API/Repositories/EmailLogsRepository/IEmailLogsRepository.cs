using EmailSender.Data.Models;

namespace EmailSenderAPI.Repositories.EmailLogsRepository;

public interface IEmailLogsRepository
{
    public Task<Email> AddLogEntryAsync(Email email);

    public Task<List<Email>> GetLogsAsync();
}
