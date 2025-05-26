using System.ComponentModel.DataAnnotations;

namespace BookApp.Services.Books;
public enum BooksSortByOptions
{
    [Display(Name = "sort by...")]
    DefaultOrder = 0,

    [Display(Name = "Votes ↑")]
    ByVotes,

    [Display(Name = "Publication Date ↑")]
    ByPublicationDate,

    [Display(Name = "Price ↓")]
    ByPriceLowestFirst,

    [Display(Name = "Price ↑")]
    ByPriceHigestFirst
}
