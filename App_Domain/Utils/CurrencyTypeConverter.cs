using Xenia.IaA.AppDomain.Entity.Model;
using Xenia.IaA.CurrencyProviderService.Data;

namespace Xenia.IaA.AppDomain.Utils;
internal static class CurrencyTypeConverter
{
    internal static List<Currency> ConvertCurrencyList(CurrencyList currencyList)
    {
        List<Currency> result = new List<Currency>();
        currencyList.Currencies.ForEach(c => result.Add(new Currency
        {
            ISOCode = c.ISOCode,
            Name = c.Name,
            ExchangeRateToTRY = c.ExchangeRate,
            RateTimestamp = currencyList.UnixTimeStamp,
        }));

        return result;
    }

    internal static Currency ConvertSingleCurrency(CurrencyData currencyData, DateTime timestamp)
    {
        return new Currency
        {
            ISOCode = currencyData.ISOCode,
            Name = currencyData.Name,
            ExchangeRateToTRY = currencyData.ExchangeRate,
            RateTimestamp = timestamp,
        };
    }
}