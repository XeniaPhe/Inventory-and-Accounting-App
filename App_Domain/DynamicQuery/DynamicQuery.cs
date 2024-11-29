using System.Linq.Expressions;
using Xenia.IaA.AppDomain.Entity.DTO.Response;
using Xenia.IaA.AppDomain.Entity.Model;
using Xenia.IaA.AppDomain.Persistence.Context;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.Base;

namespace Xenia.IaA.AppDomain.DynamicQuery;
public class DynamicQuery<Entity, EntityResponse> where Entity : class, IEntity<Entity>
    where EntityResponse : class, IResponse<Entity>
{
    private List<DynamicQueryStrategy<Entity, EntityResponse>> queryStrategies;

    private protected DynamicQuery()
    {
        queryStrategies = new List<DynamicQueryStrategy<Entity, EntityResponse>>();
    }

    public void AddQueryStrategy<Query>(Query queryStrategy) where Query : DynamicQueryStrategy<Entity, EntityResponse>
    {
        if (queryStrategy is null || !CanAddQueryStrategy<Query>())
        {
            throw new ArgumentException("Query strategy can't be added!");
        }

        queryStrategies.Add(queryStrategy);
    }

    public bool CanAddQueryStrategy<Query>() where Query : DynamicQueryStrategy<Entity, EntityResponse>
    {
        foreach (var strategy in queryStrategies)
        {
            if (!strategy.CanCoexistWith<Query>())
            {
                return false;
            }
        }

        return true;
    }

    public IEnumerable<EntityResponse> ExecuteQuery(ApplicationDbContext context,
        Expression<Func<Entity, EntityResponse>> transformationExpr, IQueryable<Entity>? entities = null)
    {
        // Optimize query execution by ordering them:
        // 1- Filtering queries with no prerequisites
        // 2- For each filtering query with prerequisites in the order they are registered (selection queries):
        //      2.1- Prerequisite(s) + filtering query
        // 3- Rest of the selection queries
        // 4- Ordering queries

        entities ??= context.Set<Entity>();
        IQueryable<EntityResponse> responses = entities.Select(transformationExpr);
        queryStrategies.ForEach(query => responses = query.BuildQuery(context, responses));
        return responses;
    }
}