using System.Linq.Expressions;
using WebAppEssentials.Enums;

namespace WebAppEssentials.WebApi.Data.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// This extension method allows ordering a collection of objects based on a specified property.
    /// </summary>
    /// <typeparam name="T">The type of the objects in the collection.</typeparam>
    /// <param name="source">The collection of objects to be ordered.</param>
    /// <param name="propertyName">The name of the property to order by.</param>
    /// <param name="descending">A flag indicating whether the ordering should be in descending order. Default is false.</param>
    /// <param name="anotherLevel">A flag indicating whether this method is being called as part of a multi-level ordering. Default is false.</param>
    /// <returns>An ordered collection of objects based on the specified property.</returns>
    public static IOrderedQueryable<T> Order<T>(this IQueryable<T> source, string propertyName, SortDirection descending, bool anotherLevel = false)
    {
        var param = Expression.Parameter(typeof(T), string.Empty);
        var property = Expression.PropertyOrField(param, propertyName);
        var sort = Expression.Lambda(property, param);
        var call = Expression.Call(
            typeof(Queryable),
            (!anotherLevel ? "OrderBy" : "ThenBy") + (descending == SortDirection.Descending ? "Descending" : string.Empty),
            new[] { typeof(T), property.Type },
            source.Expression,
            Expression.Quote(sort)
        );
        return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
    }
}