using BookApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Persistence.UnitTests.Lesson2.LazyLoading;

public class LazyLoadingDbContext : DbContext
{
    public LazyLoadingDbContext(DbContextOptions<LazyLoadingDbContext> options)
        : base(options)
    {
        
    }
    
    /// <summary>
    /// Книги
    /// </summary>
    public DbSet<BookLazy> Books { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookAuthorLazy>() 
            .HasKey(x => new { BookId = x.BookLazyId, AuthorId = x.AuthorLazyId});
    }
}