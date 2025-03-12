using Ecommerce.Application.UseCases.Stmp.Commands.SendEmail;

public static class EmailDefaults
{
  public static SendEmailCommand AccountVerificationEmail(string email, string otp)
  {
    return new SendEmailCommand
    {
      To = email,
      Subject = "Account Verification",
      Body = $"Your account verification code is: {otp}",
    };
  }
}
