using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Xenia.IaA.AppDomain.Persistence.Context;
public class DateTimeToLongValueConverter : ValueConverter<DateTime, long>
{
    public DateTimeToLongValueConverter() : base((date => date.ToUniversalTime().Ticks),
        (ticks => new DateTime(ticks, DateTimeKind.Utc))) 
    {
    }
}