using CN_API.Dto.Request;
using CN_API.Interface;
using CN_API.Models;
using CN_API.Repositories;

namespace CN_API.Services;

public class TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository) : ITransactionService
{
  
    public async Task<List<Transaction>> GetTransactionsByUserId(Guid userId)
    {
        try
        {
            var transactions = await transactionRepository.GetAllTransactionsById(userId);
            return transactions.OrderByDescending( trx => trx.CreatedAt).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Transaction> CreateTransaction(Guid userId, RequestTransaction request)
    {
        try
        {
            decimal newBalanceFrom = 0;
            decimal newBalanceTo = 0;
            var user = await userRepository.GetUser(userId);
            switch (request.Action?.ToLower())
            {
                case "deposit":
                    newBalanceFrom = user.Balance + request.Amount;
                    var updateDeposit = await userRepository.UpdateUser(userId,newBalanceFrom);
                    var transactionDeposit = new Transaction(userId:userId,userId,request.Amount,updateDeposit.Balance, request.Action);
                    var createTransactionDeposit = await transactionRepository.Add(transactionDeposit);
                    return createTransactionDeposit;
                
                case "withdraw":
                    newBalanceFrom = user.Balance - request.Amount;
                    var updateWithdraw = await userRepository.UpdateUser(userId,newBalanceFrom);
                    var transactionWithdraw = new Transaction(userId:userId,userId,request.Amount,updateWithdraw.Balance, request.Action);
                    var createTransactionWithdraw = await transactionRepository.Add(transactionWithdraw);
                    return createTransactionWithdraw;
                
                case "transfer":
                    var userTo = await userRepository.GetUserByPhoneNumber(request.AccountNumber);
                    newBalanceFrom = user.Balance - request.Amount;
                    newBalanceTo = user.Balance + request.Amount;
                    var updateTransferTo = await userRepository.UpdateUser(userId,newBalanceTo);
                    var updateTransferFrom = await userRepository.UpdateUser(userId,newBalanceFrom);
                    var listTrx = new List<Transaction>();
                    listTrx.Add(new Transaction(userTo.UserId, userId, request.Amount, updateTransferTo.Balance, request.Action));
                    listTrx.Add(new Transaction(userId, userTo.UserId, request.Amount, updateTransferFrom.Balance, request.Action));
                    var createTransactionTransfer = await transactionRepository.CreateTransactionList(listTrx);
                    return createTransactionTransfer[0];
                default:
                    return null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}