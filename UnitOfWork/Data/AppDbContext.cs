using Microsoft.EntityFrameworkCore;
using UnitOfWork.Models;

namespace UnitOfWork.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    // Bu yerda DbSet larni aniqlash mumkin, masalan:
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Bu yerda model konfiguratsiyalarini aniqlash mumkin, masalan:
       
    }
}
