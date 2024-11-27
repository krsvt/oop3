using System.Text.Json.Serialization;

namespace BLL.DTO;

public record PossibleProductsResponseDTO
{
  [JsonPropertyName("products")]
  public required Dictionary<int, int> Products {get; init;}

  public PossibleProductsResponseDTO(Dictionary<int, int> p)
  {
    Products = p;
  }

  public PossibleProductsResponseDTO()
  {
    Products = new Dictionary<int, int>();
  }
}
