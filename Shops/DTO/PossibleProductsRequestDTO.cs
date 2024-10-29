using System.Text.Json.Serialization;

namespace Shops.DTO;

public record PossibleProductsRequestDTO
{
  [JsonPropertyName("money")]
  public decimal Money { get; init; }
}
