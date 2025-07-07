namespace BookApp.Services.Books.Sorting;

using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;

internal class ByVotesSorter() : BaseSorter(BooksSortByOptions.ByVotes)
{
    public override IQueryable<BookListItem> Apply(IQueryable<BookListItem> books)
        => books.OrderBy(b => b.ReviewsAverageVotes);
}