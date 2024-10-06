using Microsoft.EntityFrameworkCore;

namespace MyFirstEfCoreApp;
public class AppDbContext : DbContext
{
     
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=MyFirstEfCoreDb.db");
    }
}