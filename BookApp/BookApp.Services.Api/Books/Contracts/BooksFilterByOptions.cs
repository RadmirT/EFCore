namespace BookApp.Services.Api.Books.Contracts;

using System.ComponentModel.DataAnnotations;

public enum BooksFilterByOptions
{
    [Display(Name = "All")]
    NoFilter = 0,

    [Display(Name = "By Votes...")]
    ByVotes,

    [Display(Name = "By Categories...")]
    ByTags,

    [Display(Name = "By Year published...")]
    ByPublicationYear
}
