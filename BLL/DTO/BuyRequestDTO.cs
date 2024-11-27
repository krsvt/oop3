using System.Text.Json.Serialization;

namespace BLL.DTO;

public record BuyRequestDTO
{
  [JsonPropertyName("id")]
  public int Id {get; init;}
  [JsonPropertyName("amount")]
  public int Amount {get; init;}
}
