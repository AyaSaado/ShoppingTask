namespace ShoppingTask.Core.Tools.Mail;

public class MailService(IConfiguration config) : IMailService
{
    private readonly IConfiguration _config = config;

    public async Task SendEmail(EmailDTO request)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
        email.To.Add(MailboxAddress.Parse(request.To));
        email.Subject = request.Subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = $"{request.Body}";

        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(
            _config.GetSection("EmailHost").Value,
            465,
            SecureSocketOptions.SslOnConnect
        );

        await smtp.AuthenticateAsync(
            _config.GetSection("EmailUserName").Value,
            _config.GetSection("EmailPassword").Value
        );

        await smtp.SendAsync(email);

        await smtp.DisconnectAsync(true);
    }
}
