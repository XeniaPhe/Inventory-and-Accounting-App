using System.Linq.Expressions;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.Base;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.CommonTypes;
using Xenia.IaA.AppDomain.Entity.DTO.Response;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.ProductStrategy;
public class FilterProductsByCategoryNameStrategy : DynamicFilteringStrategy<Product, ProductResponse, string?>
{
    public FilterProductsByCategoryNameStrategy()
    {
    }

    public FilterProductsByCategoryNameStrategy(Expression<Func<ProductResponse, string?>> filteredProperty, string? filter, FilterOperator filterOperator) : base(filteredProperty, filter, filterOperator)
    {
    }
}