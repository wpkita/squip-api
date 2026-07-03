using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Squip.Rest.Infrastructure.EntityFramework;

public static class EntityFrameworkExtensions
{
    public static void AddQueryFilterToAllEntitiesAssignableFrom<T>(
        this ModelBuilder modelBuilder,
        Expression<Func<T, bool>> expression
    )
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(T).IsAssignableFrom(entityType.ClrType))
                continue;

            var parameterType = Expression.Parameter(entityType.ClrType);
            var expressionFilter = ReplacingExpressionVisitor.Replace(
                expression.Parameters.Single(),
                parameterType,
                expression.Body
            );

            foreach (var currentQueryFilter in entityType.GetDeclaredQueryFilters())
            {
                var currentExpressionFilter = ReplacingExpressionVisitor.Replace(
                    currentQueryFilter.Expression.Parameters.Single(),
                    parameterType,
                    currentQueryFilter.Expression.Body
                );
                expressionFilter = Expression.AndAlso(currentExpressionFilter, expressionFilter);
            }

            var lambdaExpression = Expression.Lambda(expressionFilter, parameterType);
            entityType.SetQueryFilter(lambdaExpression);
        }
    }
}
