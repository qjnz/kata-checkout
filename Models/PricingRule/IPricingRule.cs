namespace Kata.Checkout.Models.PricingRule
{
  public interface IPricingRule
  {
    decimal CalculatePrice(int quantity, decimal unitPrice);
  }
}
