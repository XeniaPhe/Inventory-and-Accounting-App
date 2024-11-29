using Xenia.IaA.AppDomain.Entity.Model;
using Xenia.IaA.AppDomain.Service;

namespace Xenia.IaA.AppDomain.Controller.Base;
public abstract class ControllerBase<Entity, Service> : IController<Entity, Service>
    where Entity : class, IEntity<Entity> where Service : ServiceBase<Entity>
{
    private protected Service service;

    internal static Controller CreateController<Controller>(Service service)
        where Controller : ControllerBase<Entity, Service>, new()
    {
        Controller controller = new Controller();
        controller.service = service;
        return controller;
    }
}