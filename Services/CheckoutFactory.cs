using Kata.Checkout.Models;
using Kata.Checkout.Models.PricingRule;

namespace Kata.Checkout.Services
{
  public static class CheckoutServiceFactory
  {
    public static ICheckoutService CreateStandardItems()
    {
      var items = new[] {
      new Item("A", "Item A", 50, new BulkPricingRule(3, 130)),
      new Item("B", "Item B", 30, new BulkPricingRule(2, 45)),
      new Item("C", "Item C", 20),
      new Item("D", "Item D", 15)
    };

      return new CheckoutService();
    }

    public static ICheckoutService CreateCustomItems(IEnumerable<Item> items)
    {
      return new CheckoutService();
    }
  }
}
