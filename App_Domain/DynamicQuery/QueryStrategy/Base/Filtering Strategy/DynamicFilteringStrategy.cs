using System.Linq.Expressions;
using Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.CommonTypes;
using Xenia.IaA.AppDomain.Entity.DTO.Response;
using Xenia.IaA.AppDomain.Entity.Model;
using Xenia.IaA.AppDomain.Persistence.Context;
using Xenia.IaA.AppDomain.Utils;

using FO = Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.CommonTypes.FilterOperator;

namespace Xenia.IaA.AppDomain.DynamicQuery.QueryStrategy.Base;
public abstract class DynamicFilteringStrategy<Entity, EntityResponse, Key> : DynamicQueryStrategy<Entity, EntityResponse>
    where Entity : class, IEntity<Entity> where EntityResponse : class, IResponse<Entity>
{
    private delegate Expression FilterExpression(Expression left, Expression right);
    private delegate ConstantExpression ConstExpression(Key filter);

    private readonly Expression<Func<EntityResponse, bool>> filteringExpr;

    private static readonly IReadOnlyDictionary<Type, List<FilterOperator>> AllowedOperatorsPerType = new Dictionary<Type, List<FilterOperator>>()
    {
        { typeof(int)     , new() { FO.GreaterThanOrEqual, FO.Equal, FO.NotEqual, FO.GreaterThan, FO.LessThan, FO.LessThanOrEqual } },
        { typeof(uint)    , new() { FO.GreaterThanOrEqual, FO.Equal, FO.NotEqual, FO.GreaterThan, FO.LessThan, FO.LessThanOrEqual } },
        { typeof(decimal) , new() { FO.GreaterThanOrEqual, FO.Equal, FO.NotEqual, FO.GreaterThan, FO.LessThan, FO.LessThanOrEqual } },
        { typeof(DateTime), new() { FO.GreaterThanOrEqual, FO.Equal, FO.NotEqual, FO.GreaterThan, FO.LessThan, FO.LessThanOrEqual } },
        { typeof(string)  , new() { FO.Contains          , FO.Equal, FO.NotEqual, FO.StartsWith , FO.EndsWith,                    } },
    };

    private static readonly IReadOnlyDictionary<FilterOperator, (FilterExpression FilterExpr, ConstExpression ConstExpr)> OperatorExpressionMap =
        new Dictionary<FilterOperator, (FilterExpression, ConstExpression)>
        {
            { FO.Equal             , (Expression.Equal             , (filter => Expression.Constant(filter       , typeof(Key)))) },
            { FO.NotEqual          , (Expression.NotEqual          , (filter => Expression.Constant(filter       , typeof(Key)))) },
            { FO.GreaterThan       , (Expression.GreaterThan       , (filter => Expression.Constant(filter       , typeof(Key)))) },
            { FO.GreaterThanOrEqual, (Expression.GreaterThanOrEqual, (filter => Expression.Constant(filter       , typeof(Key)))) },
            { FO.LessThan          , (Expression.LessThan          , (filter => Expression.Constant(filter       , typeof(Key)))) },
            { FO.LessThanOrEqual   , (Expression.LessThanOrEqual   , (filter => Expression.Constant(filter       , typeof(Key)))) },
            { FO.Contains          , (ExpressionUtils.Like         , (filter => Expression.Constant($"%{filter}%", typeof(Key)))) },
            { FO.StartsWith        , (ExpressionUtils.Like         , (filter => Expression.Constant($"{filter}%" , typeof(Key)))) },
            { FO.EndsWith          , (ExpressionUtils.Like         , (filter => Expression.Constant($"%{filter}" , typeof(Key)))) },
        };

    private protected DynamicFilteringStrategy(Key filter)
        : this(filter, AllowedOperatorsPerType.GetValueOrDefault(typeof(Key), new()).FirstOrDefault()) { }

    private protected DynamicFilteringStrategy(Key filter, FilterOperator filterOperator)
    {
        try
        {
            _ = FilteredPropertySelector;
            Type keyType = typeof(Key);

            if (!AllowedOperatorsPerType.ContainsKey(keyType))
            {
                throw new ArgumentException($"{keyType.Name} is not an allowed type in filter operations.");
            }

            if (!Enum.IsDefined(typeof(FilterOperator), filterOperator) || filterOperator is FilterOperator.InvalidOperator)
            {
                throw new ArgumentOutOfRangeException(nameof(filterOperator), filterOperator, "Invalid value!");
            }

            if (!AllowedOperatorsPerType[keyType].Contains(filterOperator))
            {
                throw new ArgumentException($"{keyType.Name} does not support filter operator {filterOperator.ToString()}!");
            }

            if (filterOperator is not (FilterOperator.Equal or FilterOperator.NotEqual) && filter is null)
            {
                throw new ArgumentNullException(nameof(filter), $"{nameof(filter)} cannot be null outside of equality checks!");
            }
        }
        catch
        {
            filteringExpr = (e => true);
            throw;
        }

        var expressions = OperatorExpressionMap[filterOperator];
        Expression filterExpr = expressions.FilterExpr(FilteredPropertySelector, expressions.ConstExpr(filter));
        ParameterExpression entityParamExpr = Expression.Parameter(typeof(EntityResponse), nameof(EntityResponse));
        this.filteringExpr = Expression.Lambda<Func<EntityResponse, bool>>(filterExpr, entityParamExpr);
    }

    private protected abstract Expression<Func<EntityResponse, Key>> FilteredPropertySelector { get; }

    internal sealed override IQueryable<EntityResponse> BuildQuery(ApplicationDbContext context, IQueryable<EntityResponse> entities)
    {
        return entities.Where(filteringExpr);
    }
}