using System.Linq.Expressions;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.CommonTypes;
using Xenia.IaA.AppDomain.Entity.DTO.Response;
using Xenia.IaA.AppDomain.Entity.Model;
using Xenia.IaA.AppDomain.Persistence.Context;

namespace Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.Base;
public abstract class DynamicOrderingStrategy<Entity, EntityResponse, Key> : DynamicQueryStrategy<Entity, EntityResponse>
    where Entity : class, IEntity<Entity> where EntityResponse : class, IResponse<Entity>
{
    private readonly Func<IQueryable<EntityResponse>, IQueryable<EntityResponse>> order;

    private protected DynamicOrderingStrategy() : this(OrderDirection.Ascending) { }
    private protected DynamicOrderingStrategy(OrderDirection orderDirection)
    {
        try
        {
            _ = KeySelector;

            if (!Enum.IsDefined(typeof(OrderDirection), orderDirection))
            {
                throw new ArgumentOutOfRangeException(nameof(orderDirection), orderDirection, "Invalid value!");
            }

            if (orderDirection is not (OrderDirection.Ascending or OrderDirection.Descending))
            {
                throw new ArgumentOutOfRangeException(nameof(orderDirection), orderDirection, "Unrecognized value, " +
                    $"have you updated the {nameof(OrderDirection)} enum?");
            }
        }
        catch { throw; }
        finally
        {
            this.order = orderDirection switch
            {
                OrderDirection.Ascending => (entities => entities.OrderBy(KeySelector)),
                OrderDirection.Descending => (entities => entities.OrderByDescending(KeySelector)),
                _ => (entities => entities),
            };
        }
    }

    private protected abstract Expression<Func<EntityResponse, Key>> KeySelector { get; }

    internal sealed override IQueryable<EntityResponse> BuildQuery(ApplicationDbContext context, IQueryable<EntityResponse> entities)
    {
        return order(entities);
    }
}