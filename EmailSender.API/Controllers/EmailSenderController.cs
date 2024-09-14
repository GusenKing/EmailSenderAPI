using EmailSenderAPI.Dtos;
using EmailSenderAPI.Services.EmailLogsService;
using EmailSenderAPI.Services.EmailSenderService;
using Microsoft.AspNetCore.Mvc;

namespace EmailSenderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailSenderController(
    IEmailSenderService emailSenderService,
    IEmailLogsService emailLogsService
) : ControllerBase
{
    [HttpPost]
    [Route("send")]
    public async Task<IResult> SendEmails([FromBody] EmailDto emailDto)
    {
        return await emailSenderService.SendEmailMessagesAsync(
            emailDto.EmailSubject,
            emailDto.EmailMessage,
            emailDto.AddressesList
        );
    }

    [HttpGet]
    [Route("get-logs")]
    public async Task<IResult> GetLogs()
    {
        return await emailLogsService.GetLogsAsync();
    }
}
