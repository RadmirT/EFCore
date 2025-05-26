namespace BookApp.Services.Books.Extensions;

using System.Reflection;
using BookApp.Services.Books.Sorting;

public static class BookListSortExtensions
{
    private static Dictionary<BooksSortByOptions, BaseSorter> Sorters = new();

    static BookListSortExtensions()
    {
        RegisterAllSortStrategies();
    }

    public static IQueryable<BookListItem> ApplySort(this IQueryable<BookListItem> books, BooksSortByOptions options)
    {
        return Sorters[options].Apply(books.OrderBy(b => b.BookId));
    }

    private static void RegisterAllSortStrategies()
    {
        var assembly = Assembly.GetExecutingAssembly();

        var sorterTypes = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(BaseSorter)) && !t.IsAbstract);

        foreach (var type in sorterTypes)
        {
            if (Activator.CreateInstance(type) is BaseSorter sorter)
            {
                Sorters[sorter.Options] = sorter;
            }
        }
    }
}
