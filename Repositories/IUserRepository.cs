using System.ComponentModel.DataAnnotations;
using CN_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CN_API.Repositories;


public interface IUserRepository : IRepository<User>
{
    bool VerifyUserPassword(string username, string password);
    bool VerifyPhoneNumber(string phoneNumber);
    Task<decimal> GetBalance(Guid userId);
    Task<User> UpdateUser(Guid userId, Decimal balance);
    Task<User?> GetUser(Guid userId);
    Task<User?> GetUser(string username);
    
    Task<User?> GetUserByPhoneNumber(string phoneNumber);
    Task<List<User>> GetAll();
}

public class UserRepository(BankContext context) : IUserRepository
{   
    public async Task<User> Add(User data)
    {
        var result = context.Users.Add(data).Entity;
        await context.SaveChangesAsync();
        return result;
    }

    public bool VerifyUserPassword(string username, string password)
    {
       return context.Users.Any(u => u.UserName == username && u.Password == password);
    }

    public bool VerifyPhoneNumber(string phoneNumber)
    {
        var result = context.Users.Any(u => u.PhoneNumber == phoneNumber);
        return result;
    }

    public async Task<Decimal>GetBalance(Guid userId)
    {
        var result = await context.Users.FirstOrDefaultAsync(user => user.UserId == userId);
        return result?.Balance ?? 0;
    }

    public async Task<User> UpdateUser(Guid userId, decimal balance)
    {
        var result = await context.Users.FirstOrDefaultAsync(user => user.UserId == userId);
        result!.Balance = balance;
        await context.SaveChangesAsync();
        return result;
    }

    public async Task<User?> GetUser(Guid userId)
    {
        var result = await context.Users.FirstOrDefaultAsync(user => user.UserId == userId);
        return result;
    }

    public async Task<User?> GetUser(string username)
    {
        var result = await context.Users.FirstOrDefaultAsync(user => user.UserName == username);
        return result;
    }

    public async Task<User?> GetUserByPhoneNumber(string phoneNumber)
    {
        var result = await context.Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
        return result;
    }

    public async Task<List<User>> GetAll()
    {
      return await context.Users.ToListAsync();
      
    }
}