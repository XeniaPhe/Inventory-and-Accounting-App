using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.Entity.DTO.Request;
public interface IRequest<Entity> : IDataTransferObject<Entity> where Entity : class, IEntity<Entity>
{
}