using System.Text.Json;

namespace Ecommerce.Application.Common.Utilities;

public static class LogPretty
{
  public static void Log<T>(T o)
  {
    Console.WriteLine("\n\n LOG PRETTY \n\n");
    Console.WriteLine(
      JsonSerializer.Serialize(o, new JsonSerializerOptions { WriteIndented = true })
    );
    Console.WriteLine("\n\n");
  }
}
