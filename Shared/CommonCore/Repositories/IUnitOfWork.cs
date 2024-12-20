using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCore.Repositories;

public interface IUnitOfWork
{
    ITransaction BeginTransaction();
    Task CommitTransaction(ITransaction transaction);
    Task RollbackTransaction(ITransaction transaction);
    Task<int> SaveChangesAsync();
}

public interface ITransaction { }
