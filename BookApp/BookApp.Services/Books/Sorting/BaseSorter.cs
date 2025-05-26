namespace BookApp.Services.Books.Sorting;
internal abstract class BaseSorter(BooksSortByOptions options)
{
    public BooksSortByOptions Options => options;

    public abstract IQueryable<BookListItem> Apply(IQueryable<BookListItem> books);
}
