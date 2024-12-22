using System.Text.Json.Serialization;

namespace CN_API.Dto.Request;

public class RequestRegister
{
    [JsonPropertyName("username")]
    public string UserName { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("lastname")]
    public string LastName { get; set; }

    [JsonPropertyName("phonenumber")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("balance")]
    public Decimal Balance { get; set; }
}