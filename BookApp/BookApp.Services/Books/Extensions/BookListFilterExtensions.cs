namespace BookApp.Services.Books.Extensions;

using System.Reflection;
using BookApp.Persistence;
using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;
using BookApp.Services.Books.Filtering;
using BookApp.Services.Books.Sorting;

public static class BookListFilterExtensions
{
    private static Dictionary<BooksFilterByOptions, BaseFilter> Filters = new();

    static BookListFilterExtensions()
    {
        RegisterAllFilters();
    }

    public static IQueryable<BookListItem> FilterBy(this IQueryable<BookListItem> books, BooksFilterByOptions options, string filterValue)
    {
        return Filters[options].Apply(books, filterValue);
    }
    
    
    public static async Task<IEnumerable<DropdownItem>> GetDropdownItems(this AppDbContext dbContext, BooksFilterByOptions options, CancellationToken cancellationToken = default)
    {
        return await Filters[options].GetDropdownItems(dbContext, cancellationToken);
    }

    private static void RegisterAllFilters()
    {
        var assembly = Assembly.GetExecutingAssembly();

        var sorterTypes = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(BaseFilter)) && !t.IsAbstract);

        foreach (var type in sorterTypes)
        {
            if (Activator.CreateInstance(type) is BaseFilter sorter)
            {
                Filters[sorter.Options] = sorter;
            }
        }
    }



}
