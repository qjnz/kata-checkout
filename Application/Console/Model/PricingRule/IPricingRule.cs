namespace Kata.Checkout;

public interface IPricingRule
{
    decimal CalculatePrice(int quantity, decimal unitPrice);
}