namespace BookApp.Persistence.UnitTests.Lesson3;

using BookApp.Entities;
using BookApp.Persistence.UnitTests.TestHelper;
using TestSupport.EfHelpers;

public class UpdatingDataTests
{
    private AppDbContext context;

    [SetUp]
    public void Setup()
    {
        var options = SqliteInMemory.CreateOptions<AppDbContext>(builder => { builder.LogTo(TestContext.WriteLine); });

        context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.SeedDatabaseFourBooks();
    }

    [TearDown]
    public void Teardown()
    {
        context.Dispose();
    }
    [Test]
    public void ChangePublishedOnDate()
    {
        var book = context.Books
            .SingleOrDefault(p =>
                p.Title == "Quantum Networking");
        if (book == null)
            throw new InvalidOperationException("Book not found");
        
        book.PublishedOn = new DateOnly(2058, 1, 1);
        
        this.context.Update(book);
        
        this.context.SaveChanges();
    }
}