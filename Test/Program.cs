using System.Text;
using Xenia.IaA.CurrencyProviderService;
using Xenia.IaA.AppDomain;

Xenia.IaA.AppDomain.AppDomain domain = new Xenia.IaA.AppDomain.AppDomain();
var currency = await domain.Initialize();

Console.WriteLine(currency.Name);
Console.WriteLine(currency.ISOCode);
Console.WriteLine(currency.ExchangeRateToTRY);

//StringBuilder sb = new StringBuilder($"Timestamp = {rates.UnixTimeStamp.ToLocalTime().ToString()}\n\n");
//sb.Append('-', 64);
//sb.Append("\n|    Currency Code   |   Currency Name    |    Currency Rate   |\n");
//sb.Append('-', 64);
//sb.Append('\n');

//foreach (var rate in rates.Currencies)
//{
//    sb.Append($"|        {rate.ISOCode}         |");
//    int margin1 = (20 - rate.Name.Length) / 2;
//    int margin2 = 20 - margin1 - rate.Name.Length;
//    sb.Append(' ', margin1);
//    sb.Append(rate.Name);
//    sb.Append(' ', margin2);
//    sb.Append('|');
//    margin1 = (20 - rate.ExchangeRate.ToString().Length) / 2;
//    margin2 = 20 - margin1 - rate.ExchangeRate.ToString().Length;
//    sb.Append(' ', margin1);
//    sb.Append(rate.ExchangeRate.ToString());
//    sb.Append(' ', margin2);
//    sb.Append("|\n");
//    sb.Append('-', 64);
//    sb.Append('\n');
//}

//Console.WriteLine(sb.ToString());