using Kata.Checkout;

namespace Console.Tests;

public class ItemTests
{
  [Fact]
  public void Items_ShouldInitializeCorrectly()
  {
    var item = new Item("A", "Item A", 50);
    Assert.Equal("A", item.Sku);
    Assert.Equal("Item A", item.Name);
    Assert.Equal(50, item.UnitPrice);
  }

  [Fact]
  public void Items_ShouldUseStandardPricingRule_WhenNoPricingRuleIsPassedIn()
  {
    var item = new Item("A", "Item A", 50);
    Assert.IsType<StandardPricingRule>(item.PricingRule);
  }

  [Fact]
  public void Items_ShouldUsePricingRule_WhenPricingRuleIsPassedIn()
  {
    var item = new Item("A", "Item A", 50, new BulkPricingRule(2, 85));
    Assert.IsType<BulkPricingRule>(item.PricingRule);
  }

  [Fact]
  public void Items_ShouldThrowException_WhenNoSkuOrNamePassedIn()
  {
    Assert.Throws<ArgumentNullException>(() => new Item(null, "Item A", 50));
    Assert.Throws<ArgumentNullException>(() => new Item("A", null, 50));
  }

  [Fact]
  public void Items_ShouldThrowException_WhenSkuOrNamePassedInIsEmpty()
  {
    Assert.Throws<ArgumentNullException>(() => new Item("", "Item A", 50));
    Assert.Throws<ArgumentNullException>(() => new Item("    ", "Item A", 50));
    Assert.Throws<ArgumentNullException>(() => new Item("A", "", 50));
    Assert.Throws<ArgumentNullException>(() => new Item("A", "     ", 0));
  }

  [Fact]
  public void Items_ShouldInitializeCorrectly_WhenUnitPriceIsZero()
  {
    var item = new Item("A", "Item A", 0);
    Assert.Equal(0, item.UnitPrice);
  }

  [Fact]
  public void Items_ShouldThrowException_WhenUnitPriceIsNegative()
  {
    Assert.Throws<ArgumentException>(() => new Item("A", "Item A", -1));
  }

  [Fact]
  public void Items_CalculatePrice_ShouldThrowException_WhenQuantityIsNegative()
  {
    var item = new Item("A", "Item A", 50);
    Assert.Throws<ArgumentException>(() => item.CalculatePrice(-1));
  }

 [Fact]
  public void Items_CalculatePrice_ShouldReturnZero_WhenQuantityIsZero()
  {
    var item = new Item("A", "Item A", 50);
    Assert.Equal(0, item.CalculatePrice(0));
  }

  [Fact]
  public void Items_CalculatePrice_ShouldReturnUnitPrice_WhenQuantityIsOne()
  {
    var item = new Item("A", "Item A", 50);
    Assert.Equal(50, item.CalculatePrice(1));
  }


  [Fact]
  public void Items_CalculatePrice_ShouldCalculateCorrectly_WhenItIsStandardPricingRule()
  {
    var item = new Item("A", "Item A", 50);
    Assert.Equal(100, item.CalculatePrice(2));
  }

  [Fact]
  public void Items_CalculatePrice_ShouldCalculateCorrectly_WhenItIsBulkPricingRule()
  {
    var item = new Item("A", "Item A", 50, new BulkPricingRule(2, 85));
    Assert.Equal(85, item.CalculatePrice(2));
  }
}
