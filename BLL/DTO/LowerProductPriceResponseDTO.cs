namespace BLL.DTO;

public record LowerProductPriceResponseDTO
{
  public int ShopId {get; init;}
  public decimal Price {get; init;}
}
