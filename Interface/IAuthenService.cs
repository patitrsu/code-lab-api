using CN_API.Dto.Request;

namespace CN_API.Interface;

public interface IAuthenService
{
    Task<string> Login(RequestLogin request);

    Guid GetGuid(HttpContext context);
}