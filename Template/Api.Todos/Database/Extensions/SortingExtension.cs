using System;
using System.Linq;
using System.Linq.Expressions;

namespace Api.Todos.Database.Extensions
{
    public static class SortingExtension
    {
        public static IQueryable<T> SortBy<T>(this IQueryable<T> q, Expression<Func<T, object>> prop, SortOrder order)
        {
            q = order switch
            {
                SortOrder.None => q,
                SortOrder.Asc => q.OrderBy(prop),
                SortOrder.Desc => q.OrderByDescending(prop),
                _ => q
            };

            return q;
        }
    }

    public enum SortOrder
    {
        None,
        Desc,
        Asc,
    }
}