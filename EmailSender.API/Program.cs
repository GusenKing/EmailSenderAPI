using EmailSender.Data;
using EmailSenderAPI.Repositories.EmailLogsRepository;
using EmailSenderAPI.Services.EmailLogsService;
using EmailSenderAPI.Services.EmailSenderService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseMongoDB(
        builder.Configuration.GetConnectionString("EmailsLogs")!,
        "EmailsLogs"
    );
    optionsBuilder.EnableSensitiveDataLogging(true);
});

builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
builder.Services.AddScoped<IEmailLogsService, EmailLogsService>();
builder.Services.AddScoped<IEmailLogsRepository, EmailLogsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
