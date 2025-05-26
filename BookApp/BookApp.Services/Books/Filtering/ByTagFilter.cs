namespace BookApp.Services.Books.Filtering;

using BookApp.Persistence;
using Microsoft.EntityFrameworkCore;

internal class ByTagFilter() : BaseFilter(BooksFilterByOptions.ByTags)
{
    public override IQueryable<BookListItem> Apply(IQueryable<BookListItem> books, string? filterValue)
    {
        return books.Where(book => book.TagStrings.Any(tag => tag == filterValue));
    }

    public override async Task<IEnumerable<DropdownItem>> GetDropdownItems(AppDbContext dbContext, CancellationToken cancellationToken = default)
    {
        return await dbContext.Tags.Select(t => new DropdownItem(t.TagId)).ToListAsync(cancellationToken: cancellationToken);   
    }
}