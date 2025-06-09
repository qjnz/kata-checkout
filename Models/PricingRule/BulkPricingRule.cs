namespace Kata.Checkout.Models.PricingRule
{
  public class BulkPricingRule: IPricingRule
  {
    public int BulkQuantity { get; set; }
    public decimal BulkPrice { get; set; }

    public BulkPricingRule(int bulkQuantity, decimal bulkPrice)
    {
      BulkQuantity = bulkQuantity;
      BulkPrice = bulkPrice;
    }

    public decimal CalculatePrice(int quantity, decimal unitPrice)
    {
      int numberOfBulk = quantity / BulkQuantity;
      int remainingItems = quantity % BulkQuantity;

      return (numberOfBulk * BulkPrice) + (remainingItems * unitPrice);
    }
  }
}