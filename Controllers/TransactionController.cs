using CN_API.Dto.Request;
using CN_API.Interface;
using CN_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CN_API.Controllers;

[Route("api/[controller]")]
public class TransactionController(IUserService userService,ITransactionService transactionService,  IAuthenService authenService) : Controller
{
    
    [HttpGet("GetTransactionById")]
    public async Task<IActionResult> GetTransactionsById()
    {
        var userId = authenService.GetGuid(HttpContext);
        var result = await transactionService.GetTransactionsByUserId(userId);
        
        return Ok(result);
    }

    [HttpPost("CreateTransaction")]
    public async Task<IActionResult> CreateTransaction([FromBody] RequestTransaction request)
    {
        try
        {
            var userId = authenService.GetGuid(HttpContext);
            if (request.AccountNumber != null)
            {
                var result = await transactionService.CreateTransaction(userId, request);
                return Ok(result);
            }

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}