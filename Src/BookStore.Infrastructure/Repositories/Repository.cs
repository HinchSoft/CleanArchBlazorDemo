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

public class Repository<TEntity> : IRepository<TEntity>
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

    public IQueryable<TEntity> ApplyFilteringByName(string field, string op, string value, IQueryable<TEntity> query)
    {
        return op switch
        {
            "=" => query.Where(p => EF.Property<string>(p, field) == value),
            "!=" => query.Where(p => EF.Property<string>(p, field) != value),
            _ => query
        };

    }

    IOrderedQueryable<TEntity> IRepository<TEntity>.ApplyOrderingByName(string field, bool asc, IQueryable<TEntity> query)
    {
        if (asc)
            return query.OrderBy(p => EF.Property<TEntity>(p, field));
        else
            return query.OrderByDescending(p => EF.Property<TEntity>(p, field));
    }

    public IOrderedQueryable<TEntity> ApplyOrderingByName(string field, bool asc, IOrderedQueryable<TEntity> query)
    {
        if (asc)
            return query.ThenBy(p => EF.Property<TEntity>(p, field));
        else
            return query.ThenByDescending(p => EF.Property<TEntity>(p, field));
    }
}
