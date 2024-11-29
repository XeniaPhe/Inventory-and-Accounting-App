namespace Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.CommonTypes;
public enum FilterOperator
{
    InvalidOperator = default,
    Equal,
    NotEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Contains,
    StartsWith,
    EndsWith,
}