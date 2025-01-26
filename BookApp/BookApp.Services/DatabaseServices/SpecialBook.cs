using BookApp.Entities;

namespace BookApp.Services.DatabaseServices;

public static class SpecialBook
{
    public static Book CreateSpecialBook()
    {
        var result = new Book
        {
            Title = "Quantum Networking",
            Description = "Entangled quantum networking provides faster-than-light data communications",
            PublishedOn = new DateOnly(2057, 1, 1),
            Price = 220,
            Publisher = "Future Press",
        };
        result.Tags.Add(new Tag { TagId = "Quantum Entanglement" });
        result.AuthorsLink.Add(new BookAuthor { Author = new Author { Name = "Future Person" }, Book = result });
        result.Reviews.Add(
            new Review
            {
                VoterName = "Jon P Smith", NumStars = 5,
                Comment = "I look forward to reading this book, if I am still alive!"
            });
        result.Reviews.Add(
            new Review
            {
                VoterName = "Albert Einstein", NumStars = 5, Comment = "I write this book if I was still alive!"
            }
        );

        result.Promotion = new PriceOffer { NewPrice = 219, PromotionalText = "Save $1 if you order 40 years ahead!" };

        return result;
    }
}