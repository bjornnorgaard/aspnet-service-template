using System.Linq.Expressions;

namespace Ast.Todos.Database.Extensions;

public static class SortingExtension
{
    public static IQueryable<T> SortBy<T>(this IQueryable<T> q, Expression<Func<T, object>> prop, SortOrder order)
    {
        q = order switch
        {
            SortOrder.Asc => q.OrderBy(prop),
            SortOrder.Desc => q.OrderByDescending(prop),
            _ => q.OrderByDescending(prop)
        };

        return q;
    }
}

public enum SortOrder
{
    Desc,
    Asc,
}