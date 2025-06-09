namespace Kata.Checkout;

public interface ICheckout
{
    void Scan(string sku);

    decimal GetTotalPrice();
}