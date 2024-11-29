namespace Xenia.IaA.AppDomain.Entity.Model;
public class SaleItem : IEntity<SaleItem>
{
    public uint ID { get; set; }
    public virtual Sale Sale { get; set; }
    public Product Product { get; set; }
    public Currency Currency { get; set; }
    public decimal ProductBuyingPrice {  get; set; }
    public decimal ProductSellingPrice { get; set; }
    public decimal CurrencyRateToTRY { get; set; }
    public uint Amount { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal VAT { get; set; }
    public decimal TotalPriceInTRY { get; set; }
    public DateTime SaleDate { get; set; }

    internal SaleItem() { }
}