using Xenia.IaA.AppDomain.Entity.Model;
using Xenia.IaA.AppDomain.Persistence.Repository;
using Xenia.IaA.AppDomain.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Xenia.IaA.AppDomain.Persistence.UoW;
internal class UnitOfWork : IUnitOfWork
{
    internal class UnitOfWorkBuilder
    {
        private UnitOfWork uow;

        internal UnitOfWorkBuilder(ApplicationDbContext context)
        {
            uow = new UnitOfWork(context);
        }

        internal UnitOfWorkBuilder ProductRepository(IGenericRepository<Product> products)
        {
            uow.Products = products;
            return this;
        }
        internal UnitOfWorkBuilder CategoryRepository(IGenericRepository<Category> categories)
        {
            uow.Categories = categories;
            return this;
        }
        internal UnitOfWorkBuilder ProducerRepository(IGenericRepository<Producer> producers)
        {
            uow.Producers = producers;
            return this;
        }
        internal UnitOfWorkBuilder CurrencyRepository(IGenericRepository<Currency> currencies)
        {
            uow.Currencies = currencies;
            return this;
        }
        internal UnitOfWorkBuilder SaleRepository(IGenericRepository<Sale> sales)
        {
            uow.Sales = sales;
            return this;
        }
        internal UnitOfWorkBuilder SaleItemRepository(IGenericRepository<SaleItem> saleItems)
        {
            uow.SaleItems = saleItems;
            return this;
        }
        internal UnitOfWorkBuilder AdjustmentRepository(IGenericRepository<Adjustment> adjustments)
        {
            uow.Adjustments = adjustments;
            return this;
        }

        internal UnitOfWorkBuilder CustomerRepository(IGenericRepository<Customer> customers)
        {
            uow.Customers = customers;
            return this;
        }
        internal UnitOfWorkBuilder KDVRepository(IGenericRepository<VAT> kdv)
        {
            uow.KDV = kdv;
            return this;
        }
        internal UnitOfWork Build()
        {
            bool completed = uow.Products != null && uow.Categories != null && uow.Producers != null
                && uow.Currencies != null && uow.Sales != null && uow.SaleItems != null
                && uow.Adjustments != null && uow.Customers != null && uow.KDV != null;

            if (!completed)
            {
                throw new InvalidOperationException("All repositories must be provided before building the UnitOfwork.");
            }

            return uow;
        }
    }

    private readonly ApplicationDbContext context;
    private IDbContextTransaction? transaction;

    public IGenericRepository<Product> Products { get; private set; }
    public IGenericRepository<Category> Categories { get; private set; }
    public IGenericRepository<Producer> Producers { get; private set; }
    public IGenericRepository<Currency> Currencies { get; private set; }
    public IGenericRepository<Sale> Sales { get; private set; }
    public IGenericRepository<SaleItem> SaleItems { get; private set; }
    public IGenericRepository<Adjustment> Adjustments { get; private set; }
    public IGenericRepository<Customer> Customers { get; private set; }
    public IGenericRepository<VAT> KDV { get; private set; }

    private UnitOfWork(ApplicationDbContext context) 
    {
        this.context = context;
        this.transaction = null;
    }

    public async Task CommitAsync()
    {
        if (transaction is not null)
        {
            try
            {
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
                transaction = null;
            }
        }
        else if (context.ChangeTracker.HasChanges())
        {
            await context.SaveChangesAsync();
        }
    }

    public async Task BeginTransactionAsync()
    {
        if (transaction is not null)
        {
            throw new InvalidOperationException("Transaction already started.");
        }

        transaction = await context.Database.BeginTransactionAsync();
    }

    public async Task RollbackAsync()
    {
        if (transaction is null)
        {
            throw new InvalidOperationException("Transaction not started.");
        }

        await transaction.RollbackAsync();
        await transaction.DisposeAsync();
        transaction = null;
    }
}