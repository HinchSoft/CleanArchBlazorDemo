using BookStore.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Expressions;

namespace BookStore.Application.Data;

public static class ContextExtensions
{
    public static IQueryable<T> Page<T>(this IQueryable<T> query,int skip,int take, Expression<Func<T,object>> keySelector,string? orderBy=null)
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            query = query.OrderBy(keySelector);
        }
        else
        {
            //when ordering the first order returns a IOrderedQueryable that additional ordering 
            // should be appended to.
            // EF.Property is used to get the key selector of the named column
            var sortArray = orderBy.Split(',');
            IOrderedQueryable<T>? sortOrder = null;
            foreach (var sort in sortArray)
            {
                string colName;
                var ss = sort.Split('-');
                bool asc = ss[0] != "Descending";
                if (ss.Length == 1)
                    colName = ss[0];
                else
                    colName = ss[1];

                if (typeof(T).GetProperty(colName) == null)
                    continue;

                if (sortOrder is null)
                {
                    if (asc)
                        sortOrder = query.OrderBy(p => EF.Property<T>(p, colName));
                    else
                        sortOrder = query.OrderByDescending(p => EF.Property<T>(p, colName));
                }
                else
                {
                    if (asc)
                        sortOrder = sortOrder.ThenBy(p => EF.Property<T>(p, colName));
                    else
                        sortOrder = sortOrder.ThenByDescending(p => EF.Property<T>(p, colName));
                }
            }
            if (sortOrder is null)
                query = query.OrderBy(keySelector);
            else
                query = sortOrder.ThenBy(keySelector);
        }

        // Skip and take to be after any ordering
        if (skip > 0)
        {
            query = query.Skip(skip);
        }
        if (take > 0)
        {
            query = query.Take(take);
        }

        return query;
    }
}
