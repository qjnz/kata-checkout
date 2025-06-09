using Kata.Checkout;

namespace Console.Tests;

public class CheckoutTests
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

    [Theory]
    [InlineData("A", 50)]
    public void SingleItems_ShouldReturnCorrectPrice(string items, decimal expectedPrice) {
    }

    [Theory]
    [InlineData("AA", 50)]
    public void MultipleItemA_ShouldApplySpecialPrice(string items, decimal expectedPrice) {
    }

    [Theory]
    [InlineData("BB", 45)]
    public void MultipleItemB_ShouldApplySpecialPrice(string items, decimal expectedPrice) {
    }

    [Theory]
    [InlineData("CC", 45)]
    public void MultipleItemC_ShouldNotApplySpecialPrice(string items, decimal expectedPrice) {
    }

    [Theory]
    [InlineData("DD", 45)]
    public void MultipleItemD_ShouldNotApplySpecialPrice(string items, decimal expectedPrice) {
    }

    [Theory]
    [InlineData("DD", 45)]
    public void MixedItems_ShouldCalculateTotalCorrectly(string items, decimal expectedPrice) {
    }

    [Theory]
    [InlineData("DD", 45)]
    public void ScanningItemsInAnyOrder_ShouldReturnTheSameTotal(string items, decimal expectedPrice) {
    }

    [Fact]
    public void RepeatedGetTotal_ShouldReturnTheSameValue() {
    }

    [Fact]
    public void CustomPricingRules_ShouldWorkCorrectly() {

    }

    [Fact]
    public void CheckoutWithLargeNumberOfItems_ShouldCalculateCorrectly() {

    }


}
