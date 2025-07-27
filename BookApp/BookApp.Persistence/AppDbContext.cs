using BookApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Persistence;

/// <summary>
/// Контекст доступа к данным
/// </summary>
public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }
    /// <summary>
    /// Книги
    /// </summary>
    public DbSet<Book> Books { get; init; }
    /// <summary>
    /// Авторы
    /// </summary>
    public DbSet<Author> Authors { get; init; }
    
    /// <summary>
    /// Теги
    /// </summary>
    public DbSet<Tag> Tags { get; init; }

    /// <summary>
    /// Акционные цены
    /// </summary>
    public DbSet<PriceOffer> PriceOffers { get; init; }
    
    /// <summary>
    /// Заказы
    /// </summary>
    public DbSet<Order> Orders { get; init; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookAuthor>() 
            .HasKey(x => new {x.BookId, x.AuthorId});
    }
}