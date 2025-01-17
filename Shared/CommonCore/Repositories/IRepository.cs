using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonCore.Repositories;
public interface IRepository<TEntity>
    where TEntity : Entity
{
    void Add(TEntity entity);
    IOrderedQueryable<TEntity> ApplyOrderingByName(string field, bool asc, IQueryable<TEntity> query);
    IOrderedQueryable<TEntity> ApplyOrderingByName(string field, bool asc, IOrderedQueryable<TEntity> query);
    IQueryable<TEntity> ApplyFilteringByName(string field, string op, string value, IQueryable<TEntity> query);
    IAsyncEnumerable<T> AsAsyncEnumerable<T>(IQueryable<T> query);
    Task<TEntity?> GetByIdAsync(int id);
    IQueryable<TEntity> GetQueryable();
    void Remove(TEntity entity);
    void Update(TEntity entity);
}

