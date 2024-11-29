using System.Linq.Expressions;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.Base;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.CommonTypes;
using Xenia.IaA.AppDomain.Entity.DTO.Response;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.ProductStrategy;
public class OrderProductsByLastPriceUpdateStrategy : DynamicOrderingStrategy<Product, ProductResponse, DateTime>
{
    public OrderProductsByLastPriceUpdateStrategy() { }
    public OrderProductsByLastPriceUpdateStrategy(OrderDirection orderDirection) : base(orderDirection) { }
    private protected override Expression<Func<ProductResponse, DateTime>> KeySelector => (p => p.LastPriceUpdate);
}