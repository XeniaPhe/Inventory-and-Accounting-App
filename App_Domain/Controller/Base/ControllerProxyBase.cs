using Microsoft.EntityFrameworkCore;
using Xenia.IaA.AppDomain.Entity.Model;
using Xenia.IaA.AppDomain.Persistence.Context;
using Xenia.IaA.AppDomain.Persistence.Repository;
using Xenia.IaA.AppDomain.Persistence.UoW;
using Xenia.IaA.AppDomain.Service;

namespace Xenia.IaA.AppDomain.Controller.Base;
public abstract class ControllerProxyBase<Entity, Controller, Service> : IController<Entity, Service>
    where Entity : class, IEntity<Entity> where Service : ServiceBase<Entity>, new()
    where Controller : ControllerBase<Entity, Service>, new()
{
    private DbContextOptions<ApplicationDbContext> contextOptions;
    private ApplicationDbContext currentContext;

    private protected ControllerProxyBase(DbContextOptions<ApplicationDbContext> contextOptions)
    {
        this.contextOptions = contextOptions;
    }

    private protected Controller GetController()
    {
        currentContext = new ApplicationDbContext(contextOptions);

        IUnitOfWork uow = new UnitOfWork.UnitOfWorkBuilder(currentContext)
                            .ProductRepository(new GenericRepository<Product>(currentContext))
                            .CategoryRepository(new GenericRepository<Category>(currentContext))
                            .ProducerRepository(new GenericRepository<Producer>(currentContext))
                            .CurrencyRepository(new GenericRepository<Currency>(currentContext))
                            .SaleRepository(new GenericRepository<Sale>(currentContext))
                            .SaleItemRepository(new GenericRepository<SaleItem>(currentContext))
                            .AdjustmentRepository(new GenericRepository<Adjustment>(currentContext))
                            .CustomerRepository(new GenericRepository<Customer>(currentContext))
                            .KDVRepository(new GenericRepository<VAT>(currentContext))
                            .Build();

        Service service = ServiceBase<Entity>.CreateService<Service>(uow);
        return ControllerBase<Entity, Service>.CreateController<Controller>(service);
    }

    private protected async Task DisposeController()
    {
        await currentContext.DisposeAsync();
    }
}