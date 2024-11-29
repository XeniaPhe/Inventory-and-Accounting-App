using Xenia.IaA.AppDomain.Entity.Model;
using Xenia.IaA.AppDomain.Persistence.UoW;

namespace Xenia.IaA.AppDomain.Service;
public abstract class ServiceBase<Entity> where Entity : class, IEntity<Entity>
{
    private protected IUnitOfWork uow;

    internal static Service CreateService<Service>(IUnitOfWork uow)
        where Service : ServiceBase<Entity>, new()
    {
        Service service = new Service();
        service.uow = uow;
        return service;
    }
}