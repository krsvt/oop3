using System.Text.Json.Serialization;

namespace BLL.DTO;

public record PossibleProductsRequestDTO
{
  [JsonPropertyName("money")]
  public decimal Money { get; init; }
}
