using System.ComponentModel.DataAnnotations;

namespace Kata.Checkout
{
  public class Item
  {
      public string Sku { get; set; }

      public string Name { get; set; }

      public decimal UnitPrice { get; set; }

      public IPricingRule PricingRule { get; set; }

      public Item(string sku, string name, decimal unitPrice, IPricingRule? pricingRule = null)
      {
        Sku = string.IsNullOrWhiteSpace(sku) ? throw new ArgumentNullException(nameof(sku)) : sku;
        Name =  string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name;
        UnitPrice = unitPrice < 0 ? throw new ArgumentException("Unit price cannot be negative", nameof(unitPrice)) : unitPrice;
        PricingRule = pricingRule ?? new StandardPricingRule();
      }

      public decimal CalculatePrice(int quantity)
      {
        if (quantity < 0)
        {
          throw new ArgumentException("Quantity must not be negative", nameof(quantity));
        }

        return PricingRule.CalculatePrice(quantity, UnitPrice);
      }


  }
}
