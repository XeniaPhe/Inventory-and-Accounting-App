using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.Entity.DTO;
public interface IDataTransferObject<Entity> where Entity : class, IEntity<Entity>
{
}