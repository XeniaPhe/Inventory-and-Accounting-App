using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.Base;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.CommonTypes;
using Xenia.IaA.AppDomain.Entity.DTO.Response;
using Xenia.IaA.AppDomain.Entity.Model;
using Xenia.IaA.AppDomain.Persistence.Context;

namespace Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.ProductStrategy;
public class SelectProductsProfitMarginStrategy : TimeConstrainedDynamicSelectionStrategy<Product, ProductResponse>
{
    public SelectProductsProfitMarginStrategy() { }
    public SelectProductsProfitMarginStrategy(TimeConstraint timeConstraint) : base(timeConstraint) { }

    internal override IEnumerable<(DynamicQueryStrategy<Product, ProductResponse> strategy, bool enforced)> Prerequisites
    {
        get
        {
            yield return (new SelectProductsCOGSStrategy(timeConstraint), true);
            yield return (new SelectProductsRevenueStrategy(timeConstraint), true);
        }
    }

    internal override IQueryable<ProductResponse> BuildQuery(ApplicationDbContext context, IQueryable<ProductResponse> entities)
    {
        return entities.Select(product => new ProductResponse
        {
            ProductCode = product.ProductCode,
            ProductName = product.ProductName,
            Description = product.Description,
            BuyingPrice = product.BuyingPrice,
            SellingPrice = product.SellingPrice,
            Stock = product.Stock,
            LastPriceUpdate = product.LastPriceUpdate,
            CategoryName = product.CategoryName,
            ProducerName = product.ProducerName,
            CurrencyCode = product.CurrencyCode,
            BuyingPriceInTRY = product.BuyingPriceInTRY,
            SellingPriceInTRY = product.SellingPriceInTRY,
            LastSaleDate = product.LastSaleDate,
            NumberOfSales = product.NumberOfSales,
            QuantitySold = product.QuantitySold,
            COGS = product.COGS,
            Revenue = product.Revenue,
            EarningsAfterTaxes = product.EarningsAfterTaxes,
                GrossProfitMargin = (product.Revenue - product.COGS) / product.Revenue,
                NetProfitMargin = (product.EarningsAfterTaxes - product.COGS) / product.Revenue,
        });
    }
}