namespace Xenia.IaA.AppDomain.Entity.Model;
public class Currency : IEntity<Currency>
{
    public string ISOCode { get; set; }
    public string Name { get; set; }
    public decimal ExchangeRateToTRY { get; set; }
    public DateTime RateTimestamp { get; set; }

    internal Currency() { }
}