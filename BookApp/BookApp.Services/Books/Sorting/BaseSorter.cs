namespace BookApp.Services.Books.Sorting;

using System.Linq;
using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;

internal abstract class BaseSorter(BooksSortByOptions options)
{
    public BooksSortByOptions Options => options;

    public abstract IQueryable<BookListItem> Apply(IQueryable<BookListItem> books);
}
