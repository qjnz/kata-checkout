using System.ComponentModel.DataAnnotations;

namespace Kata.Checkout.Models
{
  public class CartItem
  {
      public string Sku { get; set; }

      public int Quantity { get; set;}
  }
}
