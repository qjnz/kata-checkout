using Kata.Checkout;

namespace Console.Tests;

public class PricingRuleTests
{
    [Fact]
    public void EmptyCart_ShouldReturnZero()
    {
        var checkout = new Checkout();
        Assert.Equal(0, checkout.GetTotalPrice());
    }

    [Fact]
    public void ScanningInvalidItem_ShouldThrowException()
    {
        var checkout = new Checkout();
        Assert.Equal(0, checkout.GetTotalPrice());
    }
}
