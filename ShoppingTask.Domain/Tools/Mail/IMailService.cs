namespace ShoppingTask.Core.Tools.Mail;

public interface IMailService
{
    Task SendEmail(EmailDTO request);
}
