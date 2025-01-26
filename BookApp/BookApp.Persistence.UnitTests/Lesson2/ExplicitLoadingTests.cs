using BookApp.Entities;
using BookApp.Persistence.UnitTests.TestHelper;
using TestSupport.EfHelpers;

namespace BookApp.Persistence.UnitTests.Lesson2;

[TestFixture]
public class ExplicitLoadingTests
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
    public void LoadBook_Explicit()
    {
        var book = context.Books.First();
        
        context.Entry(book).Collection(o => o.AuthorsLink).Load();
        foreach (var authorLink in book.AuthorsLink)
        {
            context.Entry(authorLink)
                .Reference(bookAuthor => bookAuthor.Author)
                .Load();
        }

        context.Entry(book).Collection(b => b.Tags).Load();
        context.Entry(book).Reference(b => b.Promotion).Load();
    }

}