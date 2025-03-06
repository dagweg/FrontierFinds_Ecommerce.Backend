namespace Ecommerce.UnitTests;

public partial class Utils
{
  public record JwtSettings
  {
    public static int ExpiryMinutes { get; } = 60;
    public static string Issuer { get; } = "Ecommerce.Issuer";
    public static string Audience { get; } = "Ecommerce.Audience";
    public static string SecretKey { get; } = "secretkey-secretkey-secretkey-secretkey-secretkey";
  }
}
