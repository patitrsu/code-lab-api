using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CN_API.Repositories;

namespace CN_API.Models;

[Table("Authen")]
public class Authen : IAggregateRoot
{
    [Key]
    public Guid AuthenId { get; set; }

    public string? AccessToken { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public DateTime ExpiredAt { get; set; }
}