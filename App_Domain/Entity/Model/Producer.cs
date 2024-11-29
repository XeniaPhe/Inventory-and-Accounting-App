namespace Xenia.IaA.AppDomain.Entity.Model;
public class Producer : IEntity<Producer>
{
    public uint ID { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();

    internal Producer() { }
}