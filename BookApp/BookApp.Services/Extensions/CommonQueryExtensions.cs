namespace BookApp.Services.Extensions;

internal static class CommonQueryExtensions
{
    public static IQueryable<T> Page<T>(this IQueryable<T> query, int page, int pageSize)
    {
        if (pageSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");
        }

        if (page <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(page), "Page size must be greater than zero.");
        }
        return query.Skip((page - 1) * pageSize).Take(pageSize);
    }
}