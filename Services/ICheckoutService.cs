using Kata.Checkout.Models;

namespace Kata.Checkout.Services
{
  public interface ICheckoutService
  {
    void Scan(string sku);

    decimal GetTotalPrice();

    IEnumerable<Item> GetAvailableItems();

    IEnumerable<CartItem> GetCartItems();
  }
}
