namespace Xenia.IaA.CurrencyProviderService.Data;
public class CurrencyList
{
    public DateTime UnixTimeStamp { get; set; }
    public List<CurrencyData> Currencies { get; set; } = new List<CurrencyData>();

    public CurrencyList(DateTime timestamp)
    {
        this.UnixTimeStamp = timestamp;
    }
}