using System.Linq.Expressions;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.Base;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.CommonTypes;
using Xenia.IaA.AppDomain.Entity.DTO.Response;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.ProductStrategy;
public sealed class OrderProductsByPriceStrategy : DynamicOrderingStrategy<Product, ProductResponse, decimal>
{
    public enum PriceType
    {
        BuyingPriceInTRY,
        SellingPriceInTRY,
    }

    private readonly PriceType priceType;

    public OrderProductsByPriceStrategy() : this(OrderDirection.Ascending, PriceType.BuyingPriceInTRY) { }
    public OrderProductsByPriceStrategy(OrderDirection orderDirection, PriceType priceType) : base(orderDirection)
    {
        if (!Enum.IsDefined(typeof(PriceType), priceType))
        {
            throw new ArgumentOutOfRangeException(nameof(priceType), priceType, "Invalid value!");
        }

        this.priceType = priceType;
    }

    private protected override Expression<Func<ProductResponse, decimal>> KeySelector => priceType switch
    {
        PriceType.BuyingPriceInTRY => (product => product.BuyingPriceInTRY),
        PriceType.SellingPriceInTRY => (product => product.SellingPriceInTRY),
        _ => throw new ArgumentOutOfRangeException(nameof(priceType), priceType, "Unrecognized value, " +
                $"have you updated the {nameof(PriceType)} enum?"),
    };
}