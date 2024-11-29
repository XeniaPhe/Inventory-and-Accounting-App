using System.Linq.Expressions;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.Base;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.CommonTypes;
using Xenia.IaA.AppDomain.Entity.DTO.Response;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.ProductStrategy;
public class OrderProductsByCOGSStrategy : DynamicOrderingStrategy<Product, ProductResponse, decimal?>
{
    public OrderProductsByCOGSStrategy() { }
    public OrderProductsByCOGSStrategy(OrderDirection orderDirection) : base(orderDirection) { }
    private protected override Expression<Func<ProductResponse, decimal?>> KeySelector => (p => p.COGS);
}