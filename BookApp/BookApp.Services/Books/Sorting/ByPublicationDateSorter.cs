namespace BookApp.Services.Books.Sorting;

internal class ByPublicationDateSorter() : BaseSorter(BooksSortByOptions.ByPublicationDate)
{
    public override IQueryable<BookListItem> Apply(IQueryable<BookListItem> books)
        => books.OrderBy(b => b.PublishedOn);
}