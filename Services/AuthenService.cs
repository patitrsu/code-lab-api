using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CN_API.Dto.Request;
using CN_API.Interface;
using CN_API.Models;
using CN_API.Repositories;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CN_API.Services;

public class AuthenService(IUserRepository userRepository,IConfiguration config ) : IAuthenService
{
    public async Task<string> Login(RequestLogin request)
    {
        if (userRepository.VerifyUserPassword(request.UserName, request.Password))
        {
            var user = await userRepository.GetUser(request.UserName);
            if (user != null)
            {
                var token = GenerateJwtToken(request.UserName, user.UserId);
                return token;
            }
        }
        return "";
    }

    public Guid GetGuid(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].ToString();
        token = token.Replace("Bearer ", "");
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token);
        var tokenStr = handler.ReadToken(token) as JwtSecurityToken;
        Guid guid = Guid.Parse(tokenStr.Claims.First(c => c.Type == "UserId").Value);
        
        return guid;
    }

    private string GenerateJwtToken(string userName, Guid userId)
    {
        string? secretKey = config["Jwt:Key"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim("username", userName),
                new Claim("UserId", userId.ToString()),
            ]),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = credentials,
            Issuer = config["JWT:Issuer"],
            Audience = config["JWT:Audience"]
        };
        var tokenHandler = new JsonWebTokenHandler();
        string token = tokenHandler.CreateToken(tokenDescriptor);
        
        return token;
    }

}