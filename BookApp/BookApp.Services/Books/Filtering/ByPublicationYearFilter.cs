namespace BookApp.Services.Books.Filtering;

using BookApp.Persistence;
using Microsoft.EntityFrameworkCore;

internal class ByPublicationYearFilter() : BaseFilter(BooksFilterByOptions.ByPublicationYear)
{
    public override IQueryable<BookListItem> Apply(IQueryable<BookListItem> books, string filterValue)
    {
        if (string.IsNullOrWhiteSpace(filterValue))
        {
            return books;
        }
        
        if (string.Equals(filterValue, SpecialFiltersValue.ComingSoon, StringComparison.OrdinalIgnoreCase))
        {
            return books.Where(b => b.PublishedOn > DateOnly.FromDateTime(DateTime.UtcNow));
        }
        
        var filterYear = int.TryParse(filterValue, out var number) ? number : (int?)null;
        
        if (filterYear is null)
        {
            return books;
        }
        
        return books.Where(b => b.PublishedOn.Year >= filterYear && b.PublishedOn <= DateOnly.FromDateTime(DateTime.UtcNow));
    }

    public override async Task<IEnumerable<DropdownItem>> GetDropdownItems(AppDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);

        var result = await dbContext.Books
            .Where(b => b.PublishedOn <= today)
            .Select(b => b.PublishedOn.Year)
            .Distinct()
            .OrderByDescending(year => year)
            .Select(year => new DropdownItem(year.ToString()))
            .ToListAsync(cancellationToken: cancellationToken);
        
        var comingSoon = await dbContext.Books.AnyAsync(b => b.PublishedOn > today, cancellationToken: cancellationToken);
        if (comingSoon)
        {
            result.Insert( 0, new DropdownItem(SpecialFiltersValue.ComingSoon, SpecialFiltersValue.ComingSoon));
        }

        return result;
    }
}