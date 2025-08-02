namespace TaskManagerWeb.Models;

public class ResendEmailModel
{
    public string Username { get; set; } = string.Empty;
    public string UserMail { get; set; } = string.Empty;
    public string UserHash { get; set; } = string.Empty;
}