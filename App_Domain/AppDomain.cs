using Microsoft.EntityFrameworkCore;
using Xenia.IaA.CurrencyProviderService;
using Xenia.IaA.CurrencyProviderService.Data;
using Xenia.IaA.AppDomain.Entity.Model;
using Xenia.IaA.AppDomain.Utils;
using Xenia.IaA.AppDomain.Exceptions;
using Xenia.IaA.AppDomain.Persistence;
using Xenia.IaA.AppDomain.Persistence.Context;
using Xenia.IaA.AppDomain.Controller.Proxy;

namespace Xenia.IaA.AppDomain;
public class AppDomain
{
    private DbContextOptions<ApplicationDbContext> contextOptions;

    public ProductControllerProxy ProductController { get; private set; }
    public CategoryControllerProxy CategoryController { get; private set; }
    public ProducerControllerProxy ProducerController { get; private set; }
    public CurrencyControllerProxy CurrencyController { get; private set; }
    public SaleControllerProxy SaleController { get; private set; }
    public SaleItemControllerProxy SaleItemController { get; private set; }
    public AdjustmentControllerProxy AdjustmentController { get; private set; }
    public CustomerControllerProxy CustomerController { get; private set; }
    public VATControllerProxy VATController { get; private set; }

    public AppDomain()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlite(DatabaseManager.GetConnectionString());
        optionsBuilder.UseLazyLoadingProxies();
        contextOptions = optionsBuilder.Options;
        
        ProductController = new ProductControllerProxy(contextOptions);
        CategoryController = new CategoryControllerProxy(contextOptions);
        ProducerController = new ProducerControllerProxy(contextOptions);
        CurrencyController = new CurrencyControllerProxy(contextOptions);
        SaleController = new SaleControllerProxy(contextOptions);
        SaleItemController = new SaleItemControllerProxy(contextOptions);
        AdjustmentController = new AdjustmentControllerProxy(contextOptions);
        CustomerController = new CustomerControllerProxy(contextOptions);
        VATController = new VATControllerProxy(contextOptions);
    }

    public async Task Initialize()
    {
        using ApplicationDbContext context = new ApplicationDbContext(contextOptions);

        if (!await context.Database.EnsureCreatedAsync())
            throw new DatabaseCreationException("Database could not be created!");

        if (await context.Currencies.AnyAsync())
            return;

        CurrencyProvider currencyProvider = new CurrencyProvider();
        CurrencyList currencyList = await currencyProvider.GetCurrentExchangeRates();
        List<Currency> currencies = CurrencyTypeConverter.ConvertCurrencyList(currencyList);

        foreach (Currency currency in currencies)
        {
            await context.Currencies.AddAsync(currency);
        }

        await context.SaveChangesAsync();
    }
}