using Kata.Checkout;

namespace Console.Tests;

public class PricingRuleTests
{
    [Fact]
    public void StandardPricingRule_ShouldCalculateCorrectly()
    {
        var standardPricingRule = new StandardPricingRule();
        Assert.Equal(50, standardPricingRule.CalculatePrice(1, 50));
        Assert.Equal(100, standardPricingRule.CalculatePrice(2, 50));
        Assert.Equal(150, standardPricingRule.CalculatePrice(3, 50));
        Assert.Equal(450, standardPricingRule.CalculatePrice(9, 50));
    }

    [Fact]
    public void BulkPricingRule_ShouldCalculateCorrectly()
    {
        var bulkPricingRule = new BulkPricingRule(2, 85);
        Assert.Equal(50, bulkPricingRule.CalculatePrice(1, 50));
        Assert.Equal(85, bulkPricingRule.CalculatePrice(2, 50));
        Assert.Equal(135, bulkPricingRule.CalculatePrice(3, 50));
        Assert.Equal(170, bulkPricingRule.CalculatePrice(4, 50));
    }
}
