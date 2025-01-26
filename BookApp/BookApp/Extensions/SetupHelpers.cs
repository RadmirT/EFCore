using BookApp.Persistence;
using BookApp.Services;
using BookApp.Services.DatabaseServices;

namespace BookApp.Extensions;

public static class SetupHelpers
{
    private const string SeedDataSearchName = "Apress books*.json";
    public const string SeedFileSubDirectory = "seedData";

    public static async Task<int> SeedDatabaseIfNoBooksAsync(this AppDbContext context, string dataDirectory)
    {
        var numBooks = context.Books.Count();
        if (numBooks == 0)
        {
            //Если база данных пустая, то загружаем данные из JSON
            var books = BookJsonLoader.LoadBooks(Path.Combine(dataDirectory, SeedFileSubDirectory),
                SeedDataSearchName).ToList();
            context.Books.AddRange(books);
            await context.SaveChangesAsync();

            //Мы добавляем эту книгу отдельно, чтобы у нее был самый высокий Id. Это заставит ее появиться в верхней части списка по умолчанию
            context.Books.Add(SpecialBook.CreateSpecialBook());
            await context.SaveChangesAsync();
            numBooks = books.Count + 1;
        }

        return numBooks;
    }
}