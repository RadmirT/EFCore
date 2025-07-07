namespace BookApp.Services.Books.Sorting;

using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;

internal class DefaultSorter() : BaseSorter(BooksSortByOptions.DefaultOrder)
{
    public override IQueryable<BookListItem> Apply(IQueryable<BookListItem> books)
        => books.OrderBy(b => b.BookId);
}