namespace BookApp.Services.Books.Sorting;

internal class DefaultSorter() : BaseSorter(BooksSortByOptions.DefaultOrder)
{
    public override IQueryable<BookListItem> Apply(IQueryable<BookListItem> books)
        => books.OrderBy(b => b.BookId);
}