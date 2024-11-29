using Xenia.IaA.AppDomain.Entity.DTO.Response;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.Base;
public abstract class DynamicSelectionStrategy<Entity, EntityResponse> : DynamicQueryStrategy<Entity, EntityResponse>
     where Entity : class, IEntity<Entity> where EntityResponse : class, IResponse<Entity>
{
}