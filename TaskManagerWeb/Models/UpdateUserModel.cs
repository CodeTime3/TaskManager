namespace TaskManagerWeb.Models;

public class UpdateUserModel
{
    public string Username { get; set; } = string.Empty;
    public string UserOldMail { get; set; } = string.Empty;
    public string UserNewMail { get; set; } = string.Empty;
    public string UserHash { get; set; } = string.Empty;
}