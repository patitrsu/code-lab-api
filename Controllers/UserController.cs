using CN_API.Dto.Request;
using CN_API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CN_API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class UserController(IUserService userService, IAuthenService authenService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        var user = await userService.GetAll();
        
        return Ok(user);
    }
    
    [HttpGet("Balance")]
    public async Task<IActionResult> GetBalance()
    {
        var userId = authenService.GetGuid(HttpContext);
        var result = await userService.GetUser(userId);
        
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RequestRegister request)
    {
        var result = userService.RegisterUser(request);
        
        return Ok(result);
    }
}