namespace Xenia.IaA.AppDomain.Entity.Model;
public class Product : IEntity<Product>
{
    public string ProductCode { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Category? Category { get; set; }
    public Producer? Producer { get; set; }
    public Currency Currency { get; set; }
    public decimal BuyingPrice { get; set; }
    public decimal SellingPrice { get; set; }
    public int Stock { get; set; }
    public DateTime LastPriceUpdateDate { get; set; }
    public virtual ICollection<SaleItem> Sales { get; set; } = new HashSet<SaleItem>();

    internal Product() { }
}