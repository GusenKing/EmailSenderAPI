namespace EmailSenderAPI.Dtos;

public class EmailDto
{
    public string EmailSubject { get; set; } = null!;

    public string EmailMessage { get; set; } = null!;

    public List<string> AddressesList { get; set; } = null!;
}
