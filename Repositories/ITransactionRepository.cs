using CN_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CN_API.Repositories;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<List<Transaction>> GetAllTransactionsById(Guid userId);

    Task<List<Transaction>> CreateTransactionList(List<Transaction> listTransaction);
}

public class TransactionRepository(BankContext context) : ITransactionRepository
{
    public async Task<Transaction> Add(Transaction data)
    { 
        var transaction = await context.Transactions.AddAsync(data);
        await context.SaveChangesAsync();
        return transaction.Entity;
    }

    public async Task<List<Transaction>> GetAllTransactionsById(Guid userId)
    {
        var transactions = context.Transactions.Where(t => t.UserId == userId);
        return await transactions.ToListAsync();
    }

    public async Task<List<Transaction>> CreateTransactionList(List<Transaction> listTransaction)
    {
        context.Transactions.AddRange(listTransaction);
        await context.SaveChangesAsync();
        return listTransaction;
    }
}