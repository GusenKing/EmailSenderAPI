namespace EmailSender.Data.Models;

public class Email
{
    public int Id { get; set; }

    public string EmailSubject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime SentTime { get; set; }

    public string Status { get; set; } = null!;
}
