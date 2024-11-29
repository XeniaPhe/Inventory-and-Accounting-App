namespace Xenia.IaA.AppDomain.Entity.Model;
public class Sale : IEntity<Sale>
{
    public uint ID { get; set; }
    public Customer Customer { get; set; }
    public decimal TotalPriceInTRY { get; set; }
    public DateTime SaleStartDate { get; set; }
    public bool SaleConcluded { get; set; }
    public DateTime? SaleConcludedDate { get; set; }
    public virtual ICollection<SaleItem> SaleItems { get; set; } = new HashSet<SaleItem>();
    public virtual ICollection<Adjustment> Adjustments { get; set; } = new HashSet<Adjustment>();

    internal Sale() { }
}
