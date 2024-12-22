using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CN_API.Dto.Request;
using CN_API.Repositories;

namespace CN_API.Models;


[Table("User")]
public class User : IAggregateRoot
{
    private User()
    {
    }

    public User(RequestRegister requestRegister)
    {
        var date = DateTime.UtcNow;
        UserId = Guid.NewGuid();
        UserName = requestRegister.UserName;
        Password = requestRegister.Password;
        Name = requestRegister.Name;
        LastName = requestRegister.LastName;
        Email = requestRegister.Email;
        PhoneNumber = requestRegister.PhoneNumber;
        Balance = requestRegister.Balance;
        CreatedAt = date;
        UpdatedAt = date;
    }
    
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    [Required]
    public Guid UserId { get; set; }
    
    [MaxLength(255)]
    public string UserName { get; set; }

    [MaxLength(255)]
    public string Password { get; set; }

    [MaxLength(255)]
    public string Name { get; set; }

    [MaxLength(255)]
    public string LastName { get; set; }

    [MaxLength(255)]
    public string PhoneNumber { get; set; }

    [MaxLength(255)]
    public string Email { get; set; }

    public decimal Balance { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}