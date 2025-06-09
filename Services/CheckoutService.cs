using Kata.Checkout.Models;
using Kata.Checkout.Models.PricingRule;

namespace Kata.Checkout.Services
{
  public class CheckoutService : ICheckoutService
  {
    private readonly Dictionary<string, Item> _items;
    private readonly Dictionary<string, int> _itemQuantities;

    // public CheckoutService(IEnumerable<Item> items)
    public CheckoutService()
    {
      var items = new[] {
        new Item("A", "Item A", 50, new BulkPricingRule(3, 130)),
        new Item("B", "Item B", 30, new BulkPricingRule(2, 45)),
        new Item("C", "Item C", 20),
        new Item("D", "Item D", 15)
      };

      _items = items.ToDictionary(item => item.Sku, item => item, StringComparer.OrdinalIgnoreCase);
      _itemQuantities = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    }

    public void Scan(string sku)
    {
      if (string.IsNullOrWhiteSpace(sku))
      {
        throw new ArgumentException("SKU cannot be null or empty", nameof(sku));
      }

      if (!_items.ContainsKey(sku))
      {
        throw new ArgumentException($"Item with SKU '{sku} not found", nameof(sku));
      }

      _itemQuantities[sku] = _itemQuantities.GetValueOrDefault(sku, 0) + 1;
    }

    public decimal GetTotalPrice()
    {
      decimal totalPrice = 0;

      foreach (var (sku, quantity) in _itemQuantities)
      {
        if (!_items.TryGetValue(sku, out var item))
        {
          throw new InvalidOperationException($"Item with SKU '{sku}' not found");
        }

        totalPrice += item.CalculatePrice(quantity);
      }

      return totalPrice;
    }

    public IEnumerable<Item> GetAvailableItems()
    {
      return _items.Values;
    }

    public IEnumerable<CartItem> GetCartItems()
    {
      return _itemQuantities.Select(c => new CartItem { Sku = c.Key, Quantity = c.Value });
    }

    public void Clear()
    {
      _itemQuantities.Clear();
    }
  }
}
