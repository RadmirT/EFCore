using System.ComponentModel.DataAnnotations;

namespace BookApp.Services.Books;
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
