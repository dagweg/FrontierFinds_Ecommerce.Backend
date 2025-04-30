namespace Ecommerce.Application.Common.Extensions;

public static class StringExtensions
{
  /// <summary>
  /// Convert string to Enum otherwise return null
  /// </summary>
  /// <typeparam name="TEnum"></typeparam>
  /// <param name="value"></param>
  /// <returns></returns>
  public static TEnum? ConvertTo<TEnum>(this string value)
    where TEnum : struct, Enum
  {
    if (Enum.TryParse<TEnum>(value, true, out var result))
    {
      return result;
    }
    return null;
  }

  public static void Dump(this string value)
  {
    Console.WriteLine(value);
  }

  public static string PascalToCamelCase(this string value)
  {
    if (string.IsNullOrEmpty(value) || char.IsLower(value[0]))
      return value;

    return char.ToLowerInvariant(value[0]) + value[1..];
  }
}
