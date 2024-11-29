using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Xenia.IaA.AppDomain.Utils;
internal static class ExpressionUtils
{
    internal static Expression Like(Expression matchPropertyExpression, Expression patternExpression)
    {
        return Expression.Call(
            typeof(DbFunctionsExtensions),
            nameof(DbFunctionsExtensions.Like),
            null,
            Expression.Constant(EF.Functions),
            matchPropertyExpression,
            patternExpression
            );
    }
}