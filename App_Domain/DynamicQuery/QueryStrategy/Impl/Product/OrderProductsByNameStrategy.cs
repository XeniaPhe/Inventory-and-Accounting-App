using System.Linq.Expressions;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.Base;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.CommonTypes;
using Xenia.IaA.AppDomain.Entity.DTO.Response;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.ProductStrategy;
public sealed class OrderProductsByNameStrategy : DynamicOrderingStrategy<Product, ProductResponse, string>
{
    public OrderProductsByNameStrategy() { }
    public OrderProductsByNameStrategy(OrderDirection orderDirection) : base(orderDirection) { }
    private protected override Expression<Func<ProductResponse, string>> KeySelector => (p => p.ProductName);
}