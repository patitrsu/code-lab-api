using CN_API.Dto.Request;
using CN_API.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CN_API.Controllers;

[Route("api/[controller]")]
public class AuthenController(IAuthenService authenService) : Controller
{
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] RequestLogin request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await authenService.Login(request);
        if (result == "")
        {
            return BadRequest("Invalid username or password");
        }
        return Ok(new { accessToken = result });
    }
}
