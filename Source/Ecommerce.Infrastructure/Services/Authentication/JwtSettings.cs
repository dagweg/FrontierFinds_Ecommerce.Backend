namespace Ecommerce.Infrastructure.Services.Authentication;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    public required int ExpiryMinutes { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string SecretKey { get; init; }
}
