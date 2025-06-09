namespace Kata.Checkout.Tests
{
  public class CheckoutTests
  {
    private decimal GetTotalPrice(string items)
    {
      var checkout = CheckoutFactory.CreateStandardItems();
      foreach (var item in items)
      {
        checkout.Scan(item.ToString());
      }
      return checkout.GetTotalPrice();
    }

    [Fact]
    public void EmptyCart_ShouldReturnZero()
    {
      var checkout = CheckoutFactory.CreateStandardItems();
      Assert.Equal(0, checkout.GetTotalPrice());
    }

    [Fact]
    public void ScanningNullOrEmptyItem_ShouldThrowException()
    {
      var checkout = CheckoutFactory.CreateStandardItems();
      Assert.Throws<ArgumentException>(() => checkout.Scan(null));
      Assert.Throws<ArgumentException>(() => checkout.Scan(""));
      Assert.Throws<ArgumentException>(() => checkout.Scan("   "));
    }

    [Fact]
    public void ScanningInvalidItem_ShouldThrowException()
    {
      var checkout = CheckoutFactory.CreateStandardItems();
      Assert.Throws<ArgumentException>(() => checkout.Scan("INVALID"));
    }

    [Theory]
    [InlineData("A", 50)]
    [InlineData("B", 30)]
    [InlineData("C", 20)]
    [InlineData("D", 15)]
    public void SingleItems_ShouldReturnCorrectPrice(string items, decimal expectedPrice)
    {
      Assert.Equal(expectedPrice, GetTotalPrice(items));
    }

    [Theory]
    [InlineData("AA", 100)]
    [InlineData("AAA", 130)]
    [InlineData("AAAA", 180)]
    [InlineData("AAAAA", 230)]
    [InlineData("AAAAAA", 260)]
    public void MultipleItemA_ShouldApplySpecialPrice(string items, decimal expectedPrice)
    {
      Assert.Equal(expectedPrice, GetTotalPrice(items));
    }

    [Theory]
    [InlineData("BB", 45)]
    [InlineData("BBB", 75)]
    [InlineData("BBBB", 90)]
    [InlineData("BBBBB", 120)]
    [InlineData("BBBBBB", 135)]
    public void MultipleItemB_ShouldApplySpecialPrice(string items, decimal expectedPrice)
    {
      Assert.Equal(expectedPrice, GetTotalPrice(items));
    }

    [Theory]
    [InlineData("CC", 40)]
    [InlineData("CCC", 60)]
    [InlineData("CCCC", 80)]
    [InlineData("CCCCC", 100)]
    [InlineData("CCCCCC", 120)]
    public void MultipleItemC_ShouldNotApplySpecialPrice(string items, decimal expectedPrice)
    {
      Assert.Equal(expectedPrice, GetTotalPrice(items));
    }

    [Theory]
    [InlineData("DD", 30)]
    [InlineData("DDD", 45)]
    [InlineData("DDDD", 60)]
    [InlineData("DDDDD", 75)]
    [InlineData("DDDDDD", 90)]
    public void MultipleItemD_ShouldNotApplySpecialPrice(string items, decimal expectedPrice)
    {
      Assert.Equal(expectedPrice, GetTotalPrice(items));
    }

    [Theory]
    [InlineData("AB", 80)]
    [InlineData("ABC", 100)]
    [InlineData("CDBA", 115)]
    [InlineData("AAABBCD", 210)]
    [InlineData("BBCCDDAA", 215)]
    public void MixedItems_ShouldCalculateTotalCorrectly(string items, decimal expectedPrice)
    {
      Assert.Equal(expectedPrice, GetTotalPrice(items));
    }

    [Fact]
    public void ScanningItemsInAnyOrder_ShouldReturnTheSameTotal()
    {
      Assert.Equal(GetTotalPrice("ABB"), GetTotalPrice("BAB"));
      Assert.Equal(GetTotalPrice("ABB"), GetTotalPrice("BBA"));
    }

    [Fact]
    public void RepeatedGetTotal_ShouldReturnTheSameValue()
    {
      var checkout = CheckoutFactory.CreateStandardItems();
      checkout.Scan("A");
      checkout.Scan("A");
      checkout.Scan("A");
      Assert.Equal(130, checkout.GetTotalPrice());
      Assert.Equal(130, checkout.GetTotalPrice());
    }

    [Fact]
    public void CustomPricingRules_ShouldWorkCorrectly()
    {
      var checkout = CheckoutFactory.CreateCustomItems(new[] {
        new Item("A", "Item A", 50),
        new Item("B", "Item B", 30),
        new Item("C", "Item C", 20,  new BulkPricingRule(3, 50)),
        new Item("D", "Item D", 15,  new BulkPricingRule(2, 25))
      });
      checkout.Scan("A");
      checkout.Scan("B");
      checkout.Scan("C");
      checkout.Scan("C");
      checkout.Scan("C");
      checkout.Scan("D");
      checkout.Scan("D");

      Assert.Equal(155, checkout.GetTotalPrice());
    }

    [Fact]
    public void ScanningWithSkuCaseInsensitive_ShouldCalculateCorrectly()
    {
      var checkout = CheckoutFactory.CreateStandardItems();
      checkout.Scan("A");
      checkout.Scan("a");
      checkout.Scan("A");
      checkout.Scan("b");
      checkout.Scan("B");

      Assert.Equal(175, checkout.GetTotalPrice());
    }
  }
}

