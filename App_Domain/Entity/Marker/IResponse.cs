using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.Entity.DTO.Response;
public interface IResponse<Entity> : IDataTransferObject<Entity> where Entity : class, IEntity<Entity>
{
}