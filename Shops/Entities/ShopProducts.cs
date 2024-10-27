namespace Shops.Entities;

public class ShopProducts
{
  public required Shop Shop {get; set;}
  public required Product Product {get; set;}
  public int Amount {get; set;}
  public decimal Price {get; set;}
}
