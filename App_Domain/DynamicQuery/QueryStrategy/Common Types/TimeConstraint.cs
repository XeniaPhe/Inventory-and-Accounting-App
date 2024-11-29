namespace Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.CommonTypes;
public readonly struct TimeConstraint(TimeSpan timeSpan)
{
    private const double AvgYear = 365.25;
    private const double AvgMonth = AvgYear / 12;
    private const double AvgWeek = AvgYear / 52;
    
    public TimeConstraint() : this(SinceUnixEpoch()) { }

    private TimeSpan TimeSpan => timeSpan;
    internal bool IsConstant => timeSpan.CompareTo(TimeSpan.Zero) > 0;

    internal IQueryable<Entity> ApplyIfConstant<Entity>()

    public static readonly TimeConstraint SinceLastUpdate = new TimeConstraint(TimeSpan.Zero);
    public static readonly TimeConstraint AllTime = new TimeConstraint(SinceUnixEpoch());
    public static TimeConstraint FromDays(uint days) => new TimeConstraint(TimeSpan.FromDays(days));
    public static TimeConstraint FromWeeks(uint weeks) => new TimeConstraint(TimeSpan.FromDays(weeks * AvgWeek));
    public static TimeConstraint FromMonths(uint months) => new TimeConstraint(TimeSpan.FromDays(months * AvgMonth));
    public static TimeConstraint FromYears(uint years) => new TimeConstraint(TimeSpan.FromDays(years * AvgYear));
    private static TimeSpan SinceUnixEpoch() => DateTime.UtcNow.Subtract(DateTime.UnixEpoch);

    public static implicit operator TimeSpan(TimeConstraint timeConstraint) => timeConstraint.TimeSpan;
}