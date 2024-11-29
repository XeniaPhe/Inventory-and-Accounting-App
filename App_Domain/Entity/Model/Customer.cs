namespace Xenia.IaA.AppDomain.Entity.Model;
public class Customer : IEntity<Customer>
{
    public uint ID { get; set; }
    public string Name { get; set; }
    public string? BusinessName { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public virtual ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();

    internal Customer() { }
}