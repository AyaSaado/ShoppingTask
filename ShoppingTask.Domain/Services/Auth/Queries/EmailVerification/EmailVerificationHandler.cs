

using System.Net.Mail;

namespace ShoppingTask.Domain.Services.Auth;

public class EmailVerificationHandler(IMailService mailService) : IRequestHandler<EmailVerificationRequest, Result<string?>>
{
    private readonly IMailService _mailService = mailService;
    public async Task<Result<string?>> Handle(EmailVerificationRequest request, CancellationToken cancellationToken)
    {
        //Pre-SignUp Step
        try
        {
            try
            {
                var EmailVer = new MailAddress(request.Email);

                // Check if the email address ends with "@gmail.com"
                if (!EmailVer.Address.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
                {
                    return Result.Failure<string?>(new Error("400", "Email Address must be a Gmail address (ending with @gmail.com)"));
                }
            }
            catch (Exception)
            {
                return Result.Failure<string?>(new Error("400", "Invalid Email Address"));
            }

            var _random = new Random();

            var code = _random.Next(1000, 9999).ToString("D4");

            var Email = new EmailDTO(
                To: request.Email,
                Subject: "Verify Your Account To Sign Up!",
                Body: $"{code}"
            );

            await _mailService.SendEmail(Email);

            return code;
            // return code to client side to compare it with value assigned by user  
        }
        catch (Exception ex)
        {
            return Result.Failure<string?>(new Error("400", ex.Message));
        }
    }
}
