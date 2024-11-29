using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.Entity.DTO.Response;
public class ProductResponse : IResponse<Product>
{
    #region Native Fields
    public string ProductCode { get; internal set; }
    public string ProductName { get; internal set; }
    public string? Description { get; internal set; }
    public decimal BuyingPrice { get; internal set; }
    public decimal SellingPrice { get; internal set; }
    public int Stock { get; internal set; }
    public DateTime LastPriceUpdate { get; internal set; }
    #endregion

    #region Joined Fields
    public string? CategoryName { get; internal set; }
    public string? ProducerName { get; internal set; }
    public string CurrencyCode { get; internal set; }
    #endregion

    #region Calculated Fields
    public decimal BuyingPriceInTRY { get; internal set; }
    public decimal SellingPriceInTRY { get; internal set; }
    public DateTime LastSaleDate { get; internal set; }

    //These fields will have different values depending on the requested time interval (may not have values at all)
    public uint? NumberOfSales { get; internal set; }
    public uint? QuantitySold { get; internal set; }
    public decimal? COGS { get; internal set; }
    public decimal? Revenue { get; internal set; }
    public decimal? EarningsAfterTaxes { get; internal set; }
    public decimal? GrossProfitMargin { get; internal set; }
    public decimal? NetProfitMargin { get; internal set; }
    #endregion
}