namespace Kata.Checkout
{
  public class Checkout: ICheckout
  {
    private readonly Dictionary<string, Item> _items;
    private readonly Dictionary<string, int> _itemQuantities;

    public Checkout(IEnumerable<Item> items) {
      _items = items.ToDictionary(item => item.Sku, item => item, StringComparer.OrdinalIgnoreCase);
      _itemQuantities = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    }

    public void Scan(string sku)
    {
      if (string.IsNullOrWhiteSpace(sku)) {
        throw new ArgumentException("SKU cannot be null or empty", nameof(sku));
      }

      if (!_items.ContainsKey(sku)) {
        throw new ArgumentException($"Item with SKU '{sku} not found", nameof(sku));
      }

      _itemQuantities[sku] = _itemQuantities.GetValueOrDefault(sku, 0) + 1;
    }

    public decimal GetTotalPrice()
    {
      decimal totalPrice = 0;

      foreach (var (sku, quantity) in _itemQuantities) {
        if (!_items.TryGetValue(sku, out var item)) {
          throw new InvalidOperationException($"Item with SKU '{sku}' not found");
        }

        totalPrice += item.CalculatePrice(quantity);
      }

      return totalPrice;
    }

  }
}
