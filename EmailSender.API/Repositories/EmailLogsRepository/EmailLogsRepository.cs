using EmailSender.Data;
using EmailSender.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailSenderAPI.Repositories.EmailLogsRepository;

public class EmailLogsRepository(AppDbContext dbContext) : IEmailLogsRepository
{
    public async Task<Email> AddLogEntryAsync(Email email)
    {
        email.Id = await dbContext.Emails.CountAsync() + 1;
        var newEmailLog = await dbContext.Emails.AddAsync(email);
        await dbContext.SaveChangesAsync();
        return newEmailLog.Entity;
    }

    public async Task<List<Email>> GetLogsAsync()
    {
        return await dbContext.Emails.ToListAsync();
    }
}
