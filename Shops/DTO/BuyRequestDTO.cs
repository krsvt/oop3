using System.Text.Json.Serialization;

namespace Shops.DTO;

public record BuyRequestDTO
{
  [JsonPropertyName("id")]
  public int Id {get; init;}
  [JsonPropertyName("amount")]
  public int Amount {get; init;}
}
