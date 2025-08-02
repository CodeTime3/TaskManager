using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace TaskManagerWeb.EmailService;

public class SendEmailConfirmService
{
    private readonly IConfiguration _configuration;

    public SendEmailConfirmService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendMail(string userMail, string token)
    {
        var email = CreateMail(userMail, token);

        var smpt = new SmtpClient();
        smpt.Connect(_configuration.GetValue<string>("EmailSettings:EmailHost"), 587, SecureSocketOptions.StartTls);
        smpt.Authenticate(_configuration.GetValue<string>("EmailSettings:EmailUsername"), _configuration.GetValue<string>("EmailSettings:EmailPassword"));
        smpt.Send(email);
        smpt.Disconnect(true);
    }

    private MimeMessage CreateMail(string userMail, string token)
    {
        var mail = _configuration.GetSection("EmailUsername").Value;
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration.GetValue<string>("EmailSettings:EmailUsername")));
        email.To.Add(MailboxAddress.Parse(userMail));
        email.Subject = "Confirm your email";
        email.Body = new TextPart(TextFormat.Html)
        { Text = $"<a href=\"http://localhost:5021/api/account/conferma-email?token={token}\"> Click here to confirm your email </a>" };

        return email;
    }
}