using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CN_API.Repositories;

namespace CN_API.Models;


[Table("Transaction")]
public class Transaction : IAggregateRoot
{
    private Transaction()
    {
    }
    public Transaction(Guid userId, Guid fromUserId, decimal amount, decimal remain, string action)
    {
        var date = DateTime.UtcNow;
        TransactionId = Guid.NewGuid();
        UserId = userId;
        FromUserId = fromUserId;
        Action = action;
        Remain = remain;
        Amount = amount;
        CreatedAt = date;
        UpdatedAt = date;
        
    }
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    [Required]
    public Guid TransactionId { get; set; }

    [Required]
    public decimal Remain { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Action { get; set; }
    
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid FromUserId { get; set; }

    public decimal Amount { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}