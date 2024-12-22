using System.Text.Json.Serialization;

namespace CN_API.Dto.Request;

public class RequestTransaction
{
    [JsonPropertyName("amount")]
    public Decimal Amount { get; set; }

    [JsonPropertyName("accountNumber")]
    public string? AccountNumber { get; set; }
    
    [JsonPropertyName("action")] 
    public string? Action { get; set; }
}