namespace Kata.Checkout;

public static class CheckoutFactory
{
  public static ICheckout CreateStandardItems()
  {
    var items = new [] {
      new Item("A", "Item A", 50, new BulkPricingRule(3, 130)),
      new Item("B", "Item B", 30, new BulkPricingRule(2, 45)),
      new Item("C", "Item C", 20),
      new Item("D", "Item D", 15)
    };

    return new Checkout(items);
  }

  public static ICheckout CreateCustomItems(IEnumerable<Item> items)
  {
    return new Checkout(items);
  }
}