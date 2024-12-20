using BookStore.Infrastructure.Data;
using CommonCore.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookStore.Infrastructure.Repositories;

public class UnitOfWork:IUnitOfWork
{
    private readonly BookStoreContext _context;

    public UnitOfWork(BookStoreContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public ITransaction BeginTransaction()
    {
        return new Transaction
        {
            DbTransaction = _context.Database.BeginTransaction()
        };
    }

    public async Task CommitTransaction(ITransaction transaction)
    {
        var tran=transaction as Transaction;
        if (tran is null) throw new ArgumentException("Not a valid transaction object");
        
        await tran.DbTransaction.CommitAsync();
    }

    public async Task RollbackTransaction(ITransaction transaction)
    {
        var tran=transaction as Transaction;
        if (tran is null) throw new ArgumentException("Not a valid transaction object");

        await tran.DbTransaction.RollbackAsync();
    }
}

public class Transaction : ITransaction
{
    public IDbContextTransaction DbTransaction { get; init; }
}
