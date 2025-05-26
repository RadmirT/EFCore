namespace BookApp.Services.Books.Sorting;

internal class ByPriceHigestFirstSorter() : BaseSorter(BooksSortByOptions.ByPriceHigestFirst)
{
    public override IQueryable<BookListItem> Apply(IQueryable<BookListItem> books)
        => books.OrderByDescending(b => b.Price);
}