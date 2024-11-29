using System.Reflection;
using System.Text.Json;

using Xenia.IaA.ResourceManagement;
using Xenia.IaA.CurrencyProviderService.Data;
using Xenia.IaA.CurrencyProviderService.Utils;
using Xenia.IaA.CurrencyProviderService.Exception;

namespace Xenia.IaA.CurrencyProviderService;
public class CurrencyProvider
{
    private readonly HttpClient client;
    private readonly string endpoint;

    private readonly List<string> currencyStrings = new List<string>()
        {
            "TP.DK.USD.S.EF",
            "TP.DK.AUD.S.EF",
            "TP.DK.DKK.S.EF",
            "TP.DK.EUR.S.EF",
            "TP.DK.GBP.S.EF",
            "TP.DK.CHF.S.EF",
            "TP.DK.SEK.S.EF",
            "TP.DK.CAD.S.EF",
            "TP.DK.KWD.S.EF",
            "TP.DK.NOK.S.EF",
            "TP.DK.SAR.S.EF",
            "TP.DK.JPY.S.EF",
        };

    public CurrencyProvider()
    {
        string keyPath = new ResourcePathBuilder()
            .RootNamespace("Currency_Service")
            .AddFolderToHierarchy("Resources")
            .ResourceName("apikey.json")
            .Build();

        Assembly assembly = Assembly.GetExecutingAssembly();
        EmbeddedJsonReader config = new EmbeddedJsonReader(assembly, keyPath);

        string currenciesString = string.Join('-', currencyStrings);
        string today = DateTime.Now.ToString("dd-MM-yyyy");

        this.endpoint = $"series={currenciesString}&type=json&startDate={today}&endDate={today}";
        this.client = new HttpClient();
        this.client.BaseAddress = new Uri("https://evds2.tcmb.gov.tr/service/evds/");
        this.client.DefaultRequestHeaders.Add("key", config["ApiSettings:ApiKey"]);
    }

    public async Task<CurrencyList> GetCurrentExchangeRates()
    {
        HttpResponseMessage response = await client.GetAsync(endpoint);

        if (!response.IsSuccessStatusCode)
            throw new CurrencyAPIException($"EVDS API call failed with status: {response.StatusCode.ToString()}");

        string jsonString = await response.Content.ReadAsStringAsync();
        using JsonDocument jsonRates = JsonDocument.Parse(jsonString);
        return ExtractCurrencyList(jsonRates);
    }

    private CurrencyList ExtractCurrencyList(JsonDocument jsonRates)
    {
        CurrencyList currencyList = new CurrencyList(DateTime.MinValue);

        if (!jsonRates.RootElement.TryGetProperty("items", out JsonElement itemsArray))
            throw new CurrencyAPIException("'items' property could not be found!");
        if (itemsArray.GetArrayLength() != 1)
            throw new CurrencyAPIException("'items' array has an unexpected length!");

        JsonElement itemsObject = itemsArray[0];

        JsonElement.ObjectEnumerator itemsEnumerator;

        try
        {
            itemsEnumerator = itemsObject.EnumerateObject();
        }
        catch
        {
            throw new CurrencyAPIException("'items' object cannot be enumerated!");
        }

        foreach (var item in itemsEnumerator)
        {
            CurrencyData currency = new CurrencyData();

            switch (item.Name)
            {
                case "UNIXTIME":
                    currencyList.UnixTimeStamp = ParseUnixTime(item);
                    break;
                case "Tarih":
                    break;
                default:
                    currency.ExchangeRate = ParseCurrencyRate(item);
                    currency.ISOCode = ParseCurrencyCode(item.Name);
                    currency.Name = CurrencyUtils.GetCurrencyName(currency.ISOCode);
                    currencyList.Currencies.Add(currency);
                    break;
            }
        }

        if (currencyList.UnixTimeStamp == DateTime.MinValue)
            throw new CurrencyAPIException("'UNIXTIME' property is missing!");

        return currencyList;
    }

    private string ParseCurrencyCode(string currencyString)
    {
        if (currencyString.Length != currencyStrings[0].Length)
            throw new CurrencyAPIException("A currency string has an unexpected length!");

        return currencyString.Substring(6, 3);
    }

    private decimal ParseCurrencyRate(JsonProperty rateProperty)
    {
        string? rateAsString;

        try
        {
            rateAsString = rateProperty.Value.GetString();
        }
        catch
        {
            throw new CurrencyAPIException("Rate property is not of the expected type!");
        }

        if (rateAsString is null)
            throw new CurrencyAPIException("Rate property has no value!");
        if (!decimal.TryParse(rateAsString, out decimal rate))
            throw new CurrencyAPIException("Rate property cannot be converted to decimal!");

        return rate;
    }

    private DateTime ParseUnixTime(JsonProperty unixTimeProperty)
    {
        if (!unixTimeProperty.Value.TryGetProperty("$numberLong", out JsonElement unixTimeElement))
            throw new CurrencyAPIException("'$numberLong' property could not be found!");

        string? unixTimeString;

        try
        {
            unixTimeString = unixTimeElement.GetString();
        }
        catch
        {
            throw new CurrencyAPIException("'$numberLong' property is not of the expected type!");
        }

        if (unixTimeString is null)
            throw new CurrencyAPIException("'$numberLong' property has no value!");
        if (!long.TryParse(unixTimeString, out long unixTimeSeconds))
            throw new CurrencyAPIException("'$numberLong' property cannot be converted to integer!");

        return DateTimeOffset.FromUnixTimeSeconds(unixTimeSeconds).DateTime;
    }
}