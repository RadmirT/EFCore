using BookApp.Persistence.UnitTests.TestHelper;
using TestSupport.EfHelpers;

namespace BookApp.Persistence.UnitTests.Lesson2;

[TestFixture]
public class PartialLoadingTests
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
    public void LoadBook_Partial()
    {
        var books = context.Books.Select(
            book => new
            {
                book.Title,
                book.Price,
                NumReviews = book.Reviews.Count(),
            }).ToList();
    }

}