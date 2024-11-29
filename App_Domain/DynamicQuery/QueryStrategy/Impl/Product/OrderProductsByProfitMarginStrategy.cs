using System.Linq.Expressions;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.Base;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.CommonTypes;
using Xenia.IaA.AppDomain.Entity.DTO.Response;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.ProductStrategy;
public class OrderProductsByProfitMarginStrategy : DynamicOrderingStrategy<Product, ProductResponse, decimal?>
{
    private readonly FinancialMeasure profitMarginKind;

    public OrderProductsByProfitMarginStrategy() : this(FinancialMeasure.Net, OrderDirection.Ascending) { }
    public OrderProductsByProfitMarginStrategy(OrderDirection orderDirection) : this(FinancialMeasure.Net, orderDirection) { }
    public OrderProductsByProfitMarginStrategy(FinancialMeasure profitMarginKind) : this(profitMarginKind, OrderDirection.Ascending) { }
    public OrderProductsByProfitMarginStrategy(FinancialMeasure profitMarginKind, OrderDirection orderDirection) : base(orderDirection)
    {
        if (!Enum.IsDefined(typeof(FinancialMeasure), profitMarginKind))
        {
            throw new ArgumentOutOfRangeException(nameof(profitMarginKind), profitMarginKind, "Invalid value!");
        }

        this.profitMarginKind = profitMarginKind;
    }

    private protected override Expression<Func<ProductResponse, decimal?>> KeySelector
    {
        get => profitMarginKind switch
        {
            FinancialMeasure.Gross => (product => product.GrossProfitMargin),
            FinancialMeasure.Net => (product => product.NetProfitMargin),
            _ => throw new ArgumentOutOfRangeException(nameof(profitMarginKind), profitMarginKind, "Unrecognized value, " +
                $"have you updated the {nameof(FinancialMeasure)} enum?"),
        };
    }
}