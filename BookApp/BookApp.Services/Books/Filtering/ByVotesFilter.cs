namespace BookApp.Services.Books.Filtering;

using BookApp.Persistence;
using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;

internal class ByVotesFilter() : BaseFilter(BooksFilterByOptions.ByVotes)
{
    private static readonly DropdownItem[] DropdownItems =
    [
        new("4", "4 stars and up"),
        new("3", "3 stars and up"),
        new("2", "2 stars and up"),
        new("1", "1 stars and up"),
    ];

    public override IQueryable<BookListItem> Apply(IQueryable<BookListItem> books, string filterValue)
    {
        var minVote = int.TryParse(filterValue, out var vote) ? vote : 0;
        return books.Where(b => b.ReviewsAverageVotes >= minVote);
    }

    public override Task<IEnumerable<DropdownItem>> GetDropdownItems(AppDbContext dbContext, CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<DropdownItem>>(DropdownItems);
    }
}