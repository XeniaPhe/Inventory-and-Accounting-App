using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.CommonTypes;
using Xenia.IaA.AppDomain.Entity.DTO.Response;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.Base;
public abstract class TimeConstrainedDynamicSelectionStrategy<Entity, EntityResponse> : DynamicSelectionStrategy<Entity, EntityResponse>
    where Entity : class, IEntity<Entity> where EntityResponse : class, IResponse<Entity>
{
    protected readonly TimeConstraint timeConstraint;
    protected TimeConstrainedDynamicSelectionStrategy() : this(TimeConstraint.AllTime) { }
    protected TimeConstrainedDynamicSelectionStrategy(TimeConstraint timeConstraint) { this.timeConstraint = timeConstraint; }
}