using System.Data;
using CN_API.Dto.Request;
using CN_API.Interface;
using CN_API.Models;
using CN_API.Repositories;

namespace CN_API.Services;
public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<User> UpdateBalance(Guid userId, decimal balance, decimal amount  , string action)
    {
        try
        {
            decimal newBalance = 0;
            switch (action.ToLower())
            {
                case "deposit":
                    newBalance = balance + amount;
                    break;
                case "withdraw":
                    newBalance = balance - amount;
                    break;
                case "transfer":
                    newBalance = balance + amount;
                    break;
                case "current":
                    newBalance = balance - amount;
                    break;
                default:
                    break;
            }
            var result = await userRepository.UpdateUser(userId, newBalance);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<decimal> GetBalance(Guid userId)
    {
        try
        {
            return await userRepository.GetBalance(userId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public bool VerifyPhoneNumber(string phoneNumber)
    {
        try
        {
            return userRepository.VerifyPhoneNumber(phoneNumber);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<User>> GetAll()
    {
        try
        {
            var users = await userRepository.GetAll();
            return users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    

    public async Task<User?> GetUser(Guid userId)
    {
        try
        {
            var result = await userRepository.GetUser(userId);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<User?> GetUser(string phoneNumber)
    {
        try
        {
            return await userRepository.GetUserByPhoneNumber(phoneNumber);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<User> RegisterUser(RequestRegister request)
    {
        try
        {
            if (userRepository.VerifyPhoneNumber(request.PhoneNumber))
            {
                throw new Exception("Phone number is already in use");
            }

            var user = new User(request);
            var result =  await userRepository.Add(user);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}