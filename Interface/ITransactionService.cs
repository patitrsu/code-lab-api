using CN_API.Dto.Request;
using CN_API.Models;

namespace CN_API.Interface;

public interface ITransactionService
{
    Task<List<Transaction>> GetTransactionsByUserId(Guid userId);
    
    Task<Transaction> CreateTransaction(Guid userId, RequestTransaction request);
    
}