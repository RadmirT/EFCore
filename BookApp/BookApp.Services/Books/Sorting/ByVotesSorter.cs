namespace BookApp.Services.Books.Sorting;

internal class ByVotesSorter() : BaseSorter(BooksSortByOptions.ByVotes)
{
    public override IQueryable<BookListItem> Apply(IQueryable<BookListItem> books)
        => books.OrderBy(b => b.ReviewsAverageVotes);
}