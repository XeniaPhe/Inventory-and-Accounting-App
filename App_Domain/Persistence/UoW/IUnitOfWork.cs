using Xenia.IaA.AppDomain.Entity.Model;
using Xenia.IaA.AppDomain.Persistence.Repository;

namespace Xenia.IaA.AppDomain.Persistence.UoW;
internal interface IUnitOfWork
{
    IGenericRepository<Product> Products { get; }
    IGenericRepository<Category> Categories { get; }
    IGenericRepository<Producer> Producers { get; }
    IGenericRepository<Currency> Currencies { get; }
    IGenericRepository<Sale> Sales { get; }
    IGenericRepository<SaleItem> SaleItems { get; }
    IGenericRepository<Adjustment> Adjustments { get; }
    IGenericRepository<Customer> Customers { get; }
    IGenericRepository<VAT> KDV { get; }

    Task BeginTransactionAsync();
    Task RollbackAsync();
    Task CommitAsync();
}