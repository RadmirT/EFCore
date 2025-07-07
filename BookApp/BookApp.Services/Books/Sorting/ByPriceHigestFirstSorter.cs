namespace BookApp.Services.Books.Sorting;

using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;

internal class ByPriceHigestFirstSorter() : BaseSorter(BooksSortByOptions.ByPriceHigestFirst)
{
    public override IQueryable<BookListItem> Apply(IQueryable<BookListItem> books)
        => books.OrderByDescending(b => b.Price);
}