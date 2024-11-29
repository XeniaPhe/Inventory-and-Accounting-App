using Xenia.IaA.AppDomain.Entity.DTO.Response;
using Xenia.IaA.AppDomain.Entity.Model;
using Xenia.IaA.AppDomain.Persistence.Context;

namespace Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.Base;
public abstract class DynamicQueryStrategy<Entity, EntityResponse> where Entity : class, IEntity<Entity> 
    where EntityResponse : class, IResponse<Entity>
{
    internal virtual bool CanCoexistWith<Query>() where Query : DynamicQueryStrategy<Entity, EntityResponse>
    {
        if (typeof(Query).IsAssignableFrom(GetType()))
            return false;

        return true;
    }

    internal virtual IEnumerable<(DynamicQueryStrategy<Entity, EntityResponse> strategy, bool enforced)> Prerequisites => [];
    internal abstract IQueryable<EntityResponse> BuildQuery(ApplicationDbContext context, IQueryable<EntityResponse> entities);
}