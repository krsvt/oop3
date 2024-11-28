namespace BLL.DTO;

public record AddProductsRequestDTO
{
  public int ProductId {get; init;}
  public int Amount {get; init;}
  public decimal Price {get; init;}
}
