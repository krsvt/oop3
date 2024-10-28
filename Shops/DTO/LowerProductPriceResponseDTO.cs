using System.Text.Json.Serialization;

namespace Shops.DTO;

public record LowerProductPriceResponseDTO
{
  [JsonPropertyName("shopId")]
  public int ShopId {get; init;}
  [JsonPropertyName("price")]
  public decimal Price {get; init;}
}
