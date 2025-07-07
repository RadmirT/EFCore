namespace BookApp.Services.Api.Books.Contracts;

public class SortFilterPageOptions
{
    private const int DefaultPageSize = 10;

    public readonly int[] PageSizes = [5, DefaultPageSize, 20, 50, 100];

    public BooksSortByOptions OrderByOptions { get; set; }

    public BooksFilterByOptions FilterBy { get; set; }

    public string FilterValue { get; set; }

    public int PageNum { get; set; } = 1;

    public int PageSize { get; init; } = DefaultPageSize;

    public int NumPages { get; private set; }

    public string PrevCheckState { get; set; }


    public void SetupPagingData<T>(IQueryable<T> query)
    {
        this.NumPages = (int)Math.Ceiling(
            (double)query.Count() / this.PageSize);
        this.PageNum = Math.Min(
            Math.Max(1, this.PageNum), this.NumPages);

        var newCheckState = this.GenerateCheckState();
        if (this.PrevCheckState != newCheckState)
        {
            this.PageNum = 1;
            this.PrevCheckState = newCheckState;
        }
    }

    /// <summary>
    /// Генерирует строку определяющую параметры сортировки, фильтрации.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// Если это значение изменяется  по отнощению к <see cref="PrevCheckState"/> то текущая страница сбрасывается на начальную.
    /// <returns></returns>
    private string GenerateCheckState()
    {
        return $"{(int)this.FilterBy},{this.FilterValue},{this.PageSize},{this.NumPages}";
    }
}
