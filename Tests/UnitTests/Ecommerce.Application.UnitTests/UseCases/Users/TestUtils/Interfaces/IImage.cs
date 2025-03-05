namespace Ecommerce.Application.UnitTests.UseCases.Users.TestUtils.Interfaces;

public interface IImage
{
    static string FileName { get; set; } = null!;
    static string ImageData { get; set; } = null!;
    static Stream ImageStream { get; set; } = null!;
}
