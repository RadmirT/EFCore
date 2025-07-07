using BookApp.Entities;

namespace BookApp.Services.Books;

using BookApp.Services.Api.Books;
using BookApp.Services.Api.Books.Contracts;

public static class BookListItemExtensions
{
    public static IQueryable<BookListItem> MapToBookList(this IQueryable<Book> books) =>
        books.Select(
            book =>
                new BookListItem
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    PublishedOn = book.PublishedOn,
                    AuthorsOrdered = string.Join(
                        ", ",
                        book.AuthorsLink
                            .OrderBy(link => link.Order)
                            .Select(link => link.Author.Name)),
                    TagStrings = book.Tags.Select(tag => tag.TagId).ToArray(),
                    Price = book.Price,
                    ActualPrice = book.Promotion == null ? book.Price : book.Promotion.NewPrice,
                    PromotionPromotionalText = book.Promotion == null ? null : book.Promotion.PromotionalText,
                    ReviewsCount = book.Reviews.Count,
                    ReviewsAverageVotes = book.Reviews.Select(review => review.NumStars).Average(),
                });
}