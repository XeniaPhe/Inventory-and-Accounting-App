namespace Xenia.IaA.AppDomain.Entity.Model;

public class Adjustment : IEntity<Adjustment>
{
    public uint ID { get; set; }
    public virtual Sale Sale { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal AdjustmentPriceInTRY { get; set; }
    public DateTime AdjustmentDate { get; set; }

    internal Adjustment() { }
}