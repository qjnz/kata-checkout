namespace Kata.Checkout.Models.PricingRule
{
  public class StandardPricingRule: IPricingRule
  {
      public decimal CalculatePrice(int quantity, decimal unitPrice)
      {
          return quantity * unitPrice;
      }
  }
}