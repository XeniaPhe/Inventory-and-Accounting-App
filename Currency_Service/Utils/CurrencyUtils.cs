using Xenia.IaA.CurrencyProviderService.Exception;

namespace Xenia.IaA.CurrencyProviderService.Utils;
internal static class CurrencyUtils
{
    internal static string GetCurrencyName(string currencyCode)
    {
        return currencyCode switch
        {
            "USD" => "United States Dollar",
            "AUD" => "Australian Dollar",
            "DKK" => "Danish Krone",
            "EUR" => "Euro",
            "GBP" => "Pound Sterling",
            "CHF" => "Swiss Franc",
            "SEK" => "Swedish Krona",
            "CAD" => "Canadian Dollar",
            "KWD" => "Kuwaiti Dinar",
            "NOK" => "Norwegian Krone",
            "SAR" => "Saudi Riyal",
            "JPY" => "Japanese Yen",
            _ => throw new CurrencyAPIException("Unrecognized currency code.")
        };
    }
}