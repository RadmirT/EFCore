namespace BookApp.Services.Books.Sorting;

using System.Linq;
using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;

internal class ByPublicationDateSorter() : BaseSorter(BooksSortByOptions.ByPublicationDate)
{
    public override IQueryable<BookListItem> Apply(IQueryable<BookListItem> books)
        => books.OrderBy(b => b.PublishedOn);
}