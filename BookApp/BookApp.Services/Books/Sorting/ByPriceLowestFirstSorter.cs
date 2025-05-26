namespace BookApp.Services.Books.Sorting;

internal class ByPriceLowestFirstSorter() : BaseSorter(BooksSortByOptions.ByPriceLowestFirst)
{
    public override IQueryable<BookListItem> Apply(IQueryable<BookListItem> books)
        => books.OrderBy(b => b.Price);
}