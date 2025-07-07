namespace BookApp.Persistence.UnitTests.Lesson3;

using BookApp.Persistence.UnitTests.TestHelper;
using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;

[TestFixture]
public class DeletingDataTests
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
    public void DeleteSingleEntity()
    {
        var promotion = context.PriceOffers
            .First();

        context.Remove(promotion);
        context.SaveChanges();
    }

    [Test]
    public void DeleteDependentEntities()
    {
        var book = context.Books
            .Include(p => p.Promotion)
            .Include(p => p.Reviews)
            .Include(p => p.AuthorsLink)
            .Include(p => p.Tags)
            .Single(p => p.Title == "Quantum Networking");

        context.Books.Remove(book);
        context.SaveChanges();
    }
}