using BookStore.Infrastructure.Data;
using CommonCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
}
