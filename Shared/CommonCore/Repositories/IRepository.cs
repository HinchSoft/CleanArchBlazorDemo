using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCore.Repositories;
public interface IRepository<TEntity>
    where TEntity : Entity
{
    void Add(TEntity entity);
    IAsyncEnumerable<T> AsAsyncEnumerable<T>(IQueryable<T> query);
    Task<TEntity?> GetByIdAsync(int id);
    IQueryable<TEntity> GetQueryable();
    void Remove(TEntity entity);
    void Update(TEntity entity);
}
