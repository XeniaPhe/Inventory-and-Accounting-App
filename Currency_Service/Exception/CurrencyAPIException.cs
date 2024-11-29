namespace Xenia.IaA.CurrencyProviderService.Exception;
public class CurrencyAPIException : System.Exception
{
    public CurrencyAPIException(string? message) : base($"Currency API Exception:\n{message}") { }
}