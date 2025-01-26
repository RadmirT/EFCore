using BookApp.Persistence.UnitTests.TestHelper;
using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;

namespace BookApp.Persistence.UnitTests.Lesson2.LazyLoading;

[TestFixture]
public class LazyLoadingTestЮs
{
    private LazyLoadingDbContext context;
    
    [SetUp]
    public void Setup()
    {
        var options = SqliteInMemory.CreateOptions<LazyLoadingDbContext>(builder =>
        {
            builder.UseLazyLoadingProxies();
        });
        
        context = new LazyLoadingDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseDummyBooks();
    }
    
    [TearDown]
    public void Teardown()
    {
        context.Dispose();
    }
    
    [Test]
    public void LoadBook()
    {
        var book = context.Books.First();
        var reviews = book.Reviews.ToList();
        Assert.That(reviews.Count, Is.GreaterThan(0));
    }

}