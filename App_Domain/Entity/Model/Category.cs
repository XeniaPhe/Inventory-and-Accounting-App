namespace Xenia.IaA.AppDomain.Entity.Model;
public class Category : IEntity<Category>
{
    public uint ID { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();

    internal Category() { }
}