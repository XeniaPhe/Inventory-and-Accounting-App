using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.Base;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.CommonTypes;
using Xenia.IaA.AppDomain.Entity.DTO.Response;
using Xenia.IaA.AppDomain.Entity.Model;
using Xenia.IaA.AppDomain.Persistence.Context;

namespace Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.ProductStrategy;
public class SelectProductsNumberOfSalesStrategy : TimeConstrainedDynamicSelectionStrategy<Product, ProductResponse>
{
    public SelectProductsNumberOfSalesStrategy() { }
    public SelectProductsNumberOfSalesStrategy(TimeConstraint timeConstraint) : base(timeConstraint) { }

    internal override IQueryable<ProductResponse> BuildQuery(ApplicationDbContext context, IQueryable<ProductResponse> entities)
    {
        IQueryable<SaleItem> saleItemsQuery = context.SaleItems;

        if (timeConstraint.IsConstant)
        {
            DateTime constantLimitingDate = DateTime.UtcNow.Subtract(timeConstraint);
            saleItemsQuery = saleItemsQuery.Where(SI => SI.SaleDate >= constantLimitingDate);
        }
        
        var intermediateQuery = entities
            .GroupJoin(
                saleItemsQuery,
                product => product.ProductCode,
                saleItem => saleItem.Product.ProductCode,
                (product, saleItems) => new 
                { 
                    Product = product, 
                    SaleDates = saleItems.AsQueryable().Select(SI => SI.SaleDate),
                });

        if (!timeConstraint.IsConstant)
        {
            intermediateQuery = intermediateQuery.Select(inter => new
            {
                Product = inter.Product,
                SaleDates = inter.SaleDates.Where(saleDate => saleDate >= inter.Product.LastPriceUpdate)
            });
        }

        return intermediateQuery.Select(inter => new ProductResponse
        {
             ProductCode = inter.Product.ProductCode,
             ProductName = inter.Product.ProductName,
             Description = inter.Product.Description,
             BuyingPrice = inter.Product.BuyingPrice,
             SellingPrice = inter.Product.SellingPrice,
             Stock = inter.Product.Stock,
             LastPriceUpdate = inter.Product.LastPriceUpdate,
             CategoryName = inter.Product.CategoryName,
             ProducerName = inter.Product.ProducerName,
             CurrencyCode = inter.Product.CurrencyCode,
             BuyingPriceInTRY = inter.Product.BuyingPriceInTRY,
             SellingPriceInTRY = inter.Product.SellingPriceInTRY,
             LastSaleDate = inter.Product.LastSaleDate,
                NumberOfSales = (uint)inter.SaleDates.Count(),
             QuantitySold = inter.Product.QuantitySold,
             COGS = inter.Product.COGS,
             Revenue = inter.Product.Revenue,
             EarningsAfterTaxes = inter.Product.EarningsAfterTaxes,
             GrossProfitMargin = inter.Product.GrossProfitMargin,
             NetProfitMargin = inter.Product.NetProfitMargin,
        });
    }
}