using System.Text.Json.Serialization;

namespace CN_API.Dto.Request;

public class RequestLogin
{
    [JsonPropertyName("username")]
    public string UserName { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}