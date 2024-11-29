namespace Xenia.IaA.AppDomain.Entity.Model;
public class VAT : IEntity<VAT>
{
    public uint ID { get; set; }
    public decimal DefaultVAT { get; set; }

    internal VAT() { }
}