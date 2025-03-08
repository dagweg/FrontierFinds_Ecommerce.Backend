using System.Text.Json;
using System.Text.Json.Nodes;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Domain.Common.Enums;
using Ecommerce.Infrastructure.Common.Interfaces.Providers.Forex;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services.Providers.Forex;

public class ExchangeApiClient(ILogger<ExchangeApiClient> logger) : IExchangeApiClient
{
  public async Task<Result<decimal>> GetExchangeRateAsync(
    Currency fromCurrency,
    Currency toCurrency
  )
  {
    string to = toCurrency.ToString().ToLower();
    string from = fromCurrency.ToString().ToLower();

    string baseUrl =
      $"https://cdn.jsdelivr.net/npm/@fawazahmed0/currency-api@latest/v1/currencies/{from}.json";

    using HttpClient client = new();
    client.BaseAddress = new Uri(baseUrl);

    try
    {
      HttpResponseMessage response = await client.GetAsync(client.BaseAddress);

      if (response.IsSuccessStatusCode)
      {
        string content = await response.Content.ReadAsStringAsync();

        // More robust deserialization with specific type
        using JsonDocument document = JsonDocument.Parse(content);
        JsonElement root = document.RootElement;

        if (root.TryGetProperty(from.ToString(), out JsonElement fromElement))
        {
          if (fromElement.TryGetProperty(to.ToString(), out JsonElement rateElement))
          {
            if (rateElement.TryGetDecimal(out decimal rate))
            {
              return Result.Ok(rate);
            }
            else
            {
              logger.LogError("Failed to parse exchange rate to decimal.");
              return InternalError.GetResult("Invalid exchange rate format.");
            }
          }
          else
          {
            logger.LogError("Exchange rate for {to} not found for {from}.", to, from);
            return InternalError.GetResult($"Exchange rate for {to} not found.");
          }
        }
        else
        {
          logger.LogError("Exchange rates for {from} not found.", from);
          return InternalError.GetResult($"Exchange rates for {from} not found.");
        }
      }
      else
      {
        logger.LogError(
          "Failed to fetch exchange rates from API. Status code: {StatusCode}",
          response.StatusCode
        );
        return InternalError.GetResult(
          $"API request failed with status code {response.StatusCode}"
        ); // More informative error
      }
    }
    catch (JsonException ex)
    {
      logger.LogError(ex, "Error deserializing JSON response."); // Log the exception details
      return InternalError.GetResult("Error deserializing exchange rate data.");
    }
    catch (Exception ex) // Catch other potential exceptions (e.g., network issues)
    {
      logger.LogError(ex, "An unexpected error occurred while fetching exchange rates.");
      return InternalError.GetResult("An unexpected error occurred.");
    }
  }
}
