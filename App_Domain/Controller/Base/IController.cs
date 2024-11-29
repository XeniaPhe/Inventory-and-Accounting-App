using Xenia.IaA.AppDomain.Entity.Model;
using Xenia.IaA.AppDomain.Service;

namespace Xenia.IaA.AppDomain.Controller.Base;
public interface IController<Entity, Service> where Entity : class, IEntity<Entity> where Service : ServiceBase<Entity>
{
}