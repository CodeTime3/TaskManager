using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace TaskManagerWeb.Services.EmailService;

public class SendEmailAddUserProject
{
    private readonly IConfiguration _configuration;

    public SendEmailAddUserProject(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendMail(string userMail, int id)
    {
        var email = CreateMail(userMail, id);

        var smpt = new SmtpClient();
        smpt.Connect(_configuration.GetValue<string>("EmailSettings:EmailHost"), 587, SecureSocketOptions.StartTls);
        smpt.Authenticate(_configuration.GetValue<string>("EmailSettings:EmailUsername"), _configuration.GetValue<string>("EmailSettings:EmailPassword"));
        smpt.Send(email);
        smpt.Disconnect(true);
    }

    private MimeMessage CreateMail(string userMail, int id)
    {
        var mail = _configuration.GetSection("EmailUsername").Value;
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration.GetValue<string>("EmailSettings:EmailUsername")));
        email.To.Add(MailboxAddress.Parse(userMail));
        email.Subject = "Join in the project";
        email.Body = new TextPart(TextFormat.Html)
        { Text = $"<a href=\"http://localhost:5021/api/userproject/send-invite?projectid={id}\"> Click here to join in the project </a>" };

        return email;
    }
}