using Microsoft.EntityFrameworkCore;

namespace CN_API.Models;

public class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Transaction> Transactions { get; set; }
}