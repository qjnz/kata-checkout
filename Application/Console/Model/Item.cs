using System.ComponentModel.DataAnnotations;

namespace Kata.Checkout
{
  public class Item
  {
      public string Sku { get; set; }

      public string? Name { get; set; }

      public decimal UnitPrice { get; set; }

      public IPricingRule PricingRule { get; set; }

      public Item(string sku, string name, decimal unitPrice, IPricingRule? pricingRule = null)
      {
        Sku = sku;
        Name = name;
        UnitPrice = unitPrice;
        PricingRule = pricingRule ?? new StandardPricingRule();
      }

      public decimal CalculatePrice(int quantity)
      {
        if (quantity <= 0)
        {
          throw new ArgumentException("Quantity must be greater than 0", nameof(quantity));
        }

        return PricingRule.CalculatePrice(quantity, UnitPrice);
      }


  }
}
