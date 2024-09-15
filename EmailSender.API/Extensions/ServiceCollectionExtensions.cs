using EmailSender.Data;
using EmailSenderAPI.Repositories.EmailLogsRepository;
using EmailSenderAPI.Services.EmailLogsService;
using EmailSenderAPI.Services.EmailSenderService;
using Microsoft.EntityFrameworkCore;

namespace EmailSenderAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IEmailSenderService, EmailSenderService>();
        return serviceCollection.AddScoped<IEmailLogsService, EmailLogsService>();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<IEmailLogsRepository, EmailLogsRepository>();
    }

    public static IServiceCollection ConfigureDbContext(
        this IServiceCollection serviceCollection,
        IConfiguration configuration
    )
    {
        return serviceCollection.AddDbContext<AppDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseMongoDB(
                configuration.GetConnectionString("EmailsLogs")!,
                "EmailsLogs"
            );
            optionsBuilder.EnableSensitiveDataLogging();
        });
    }
}
