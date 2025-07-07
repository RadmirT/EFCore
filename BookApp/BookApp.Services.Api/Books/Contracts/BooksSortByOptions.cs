namespace BookApp.Services.Api.Books.Contracts;

using System.ComponentModel.DataAnnotations;

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
