namespace DAL.Entities;

public class ShopProducts : BaseEntity
{
  public required int ShopId { get; set; }
  public required int ProductId { get; set; }
  public int Amount { get; set; }
  public decimal Price { get; set; }

  public override string? ToString()
  {
    return $"{ShopId} {ProductId} {Amount} {Price}";
  }
}
