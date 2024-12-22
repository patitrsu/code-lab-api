using CN_API.Dto.Request;
using CN_API.Models;

namespace CN_API.Interface;

public interface IUserService
{
    Task<decimal> GetBalance(Guid userId);
    
    bool VerifyPhoneNumber(string phoneNumber);
    
    Task<List<User>> GetAll();
    
    Task<User?> GetUser(Guid userId);
    
    Task<User?> GetUser(string phoneNumber);
    
    Task<User> RegisterUser(RequestRegister request);
}