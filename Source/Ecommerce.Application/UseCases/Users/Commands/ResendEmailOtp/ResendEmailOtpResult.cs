namespace Ecommerce.Application.UseCases.Users.Commands.ResendEmailOtp;

public record ResendEmailOtpResult
{
  public required int WaitSeconds { get; init; } = 0;
  public required bool IsSuccess { get; init; }
}
