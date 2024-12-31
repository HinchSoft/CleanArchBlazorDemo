using BookStore.Infrastructure.Data;
using CommonCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories;

public class Repository<TEntity>: IRepository<TEntity>
    where TEntity : Entity
{
    private readonly BookStoreContext _context;

    public Repository(BookStoreContext context)
    {
        _context = context;
    }

    public IQueryable<TEntity> GetQueryable()
    {
        return _context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public void Add(TEntity entity)
    {
         _context.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public IAsyncEnumerable<T> AsAsyncEnumerable<T>(IQueryable<T> query)
    {
        return query.AsAsyncEnumerable();
    }

    public IQueryable<TEntity> ApplyOrderingByName(string field, bool asc, IQueryable<TEntity> query)
    {
        //when ordering the first order returns a IOrderedQueryable that additional ordering 
        // should be appended to.
        IOrderedQueryable<TEntity>? sortOrder = query as IOrderedQueryable<TEntity>;

        if (sortOrder is null)
        {
            if (asc)
                sortOrder = query.OrderBy(p => EF.Property<TEntity>(p, field));
            else
                sortOrder = query.OrderByDescending(p => EF.Property<TEntity>(p, field));
        }
        else
        {
            if (asc)
                sortOrder = sortOrder.ThenBy(p => EF.Property<TEntity>(p, field));
            else
                sortOrder = sortOrder.ThenByDescending(p => EF.Property<TEntity>(p, field));
        }

        return sortOrder;
    }

    public IQueryable<TEntity> ApplyFilteringByName(string field, string op,string value, IQueryable<TEntity> query)
    {
        return op switch
        {
            "=" => query.Where(p => EF.Property<string>(p, field) == value),
            "!=" => query.Where(p => EF.Property<string>(p, field) != value),
            _ => query
        };

    }
}
