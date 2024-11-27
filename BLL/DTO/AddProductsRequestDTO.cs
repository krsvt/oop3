using System.Text.Json.Serialization;

namespace BLL.DTO;

public record AddProductsRequestDTO
{
  [JsonPropertyName("productId")]
  public int ProductId {get; init;}
  [JsonPropertyName("amount")]
  public int Amount {get; init;}
  [JsonPropertyName("price")]
  public decimal Price {get; init;}
}
