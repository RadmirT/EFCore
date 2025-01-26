using BookApp.Persistence.UnitTests.TestHelper;
using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;

namespace BookApp.Persistence.UnitTests.Lesson2;

[TestFixture]
public class EagerLoadingTests
{
    private AppDbContext context;
    [SetUp]
    public void Setup()
    {
        var options = SqliteInMemory.CreateOptions<AppDbContext>(builder =>
        {
            builder.LogTo(TestContext.WriteLine);
        });
        
         context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseDummyBooks();
    }
    [TearDown]
    public void Teardown()
    {
        context.Dispose();
    }
       
    [Test]
    public void LoadBook_Eager_IncludeRewiews()
    {
      
        var book = context.Books
            .Include(book => book.Reviews)
            .FirstOrDefault();
        Assert.That(book, Is.Not.Null);
        Assert.That(book.Reviews, Has.No.Empty);
    }
    
    [Test]
    public void LoadBook_Eager_IncludeAllNavigationProperties()
    {
        var book = context.Books
            .Include(book => book.AuthorsLink)
                .ThenInclude(bookAuthor => bookAuthor.Author)
            .Include(book => book.Reviews)
            .Include(book => book.Tags)
            .Include(book => book.Promotion)
            .FirstOrDefault();
        Assert.That(book, Is.Not.Null);
        Assert.That(book.Reviews, Has.No.Empty);
    }
    
    [Test]
    public void LoadBook_Eager_IncludeAllNavigationPropertiesAndFilter()
    {
        var book = context.Books
            .AsNoTracking()
            .Include(book => 
                book.AuthorsLink.OrderBy(bookAuthor => bookAuthor.Order ))
            .ThenInclude(bookAuthor => bookAuthor.Author)
            .Include(book => 
                book.Reviews.Where(review=> review.NumStars > 4))
            .Include(book => book.Tags)
            .Include(book => book.Promotion)
            .FirstOrDefault(book => book.Reviews.Any(review => review.NumStars == 5));
        Assert.That(book, Is.Not.Null);
        Assert.That(book.Reviews, Has.No.Empty);
        Assert.That(book.Reviews.All(review => review.NumStars == 5), Is.True);
    }

}