namespace BookApp.Services.Books.Sorting;

using System.Linq;
using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;

internal class ByPriceLowestFirstSorter() : BaseSorter(BooksSortByOptions.ByPriceLowestFirst)
{
    public override IQueryable<BookListItem> Apply(IQueryable<BookListItem> books)
        => books.OrderBy(b => b.Price);
}