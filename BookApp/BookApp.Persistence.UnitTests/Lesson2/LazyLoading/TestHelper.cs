using BookApp.Entities;
using BookApp.Persistence.UnitTests.TestHelper;

namespace BookApp.Persistence.UnitTests.Lesson2.LazyLoading;

  public static class EfTestData
  {
      private static readonly DateOnly DummyBookStartDate = new DateOnly(2010, 1, 1);

      public static void SeedDatabaseDummyBooks(this LazyLoadingDbContext context, int numBooks = 10)
      {
          context.Books.AddRange(CreateDummyBooksLazy(numBooks));
          context.SaveChanges();
      }

      public static List<BookLazy> CreateDummyBooksLazy(int numBooks = 10, bool stepByYears = false, bool setBookId = true)
      {
          var result = new List<BookLazy>();
          var commonAuthor = new AuthorLazy {Name = "CommonAuthor"};
          for (int i = 0; i < numBooks; i++)
          {
              var reviews = new List<Review>();
              for (int j = 0; j < i + 1; j++)
              {
                  reviews.Add(new Review {VoterName = j.ToString(), NumStars = (j % 5) + 1});
              }

              var book = new BookLazy
              {
                  Title = $"Book{i:D4} Title",
                  Description = $"Book {i:D4} Description",
                  Price = (short)(i + 1),
                  ImageUrl = $"Image {i:D4}",
                  PublishedOn = stepByYears
                      ? DummyBookStartDate.AddYears(i)
                      : DummyBookStartDate.AddDays(i),
                  Publisher = $"Book {i:D4} Publisher",
              };
              book.Reviews.AddRange(reviews);
        
              var author = new AuthorLazy {Name = $"Author {i:D4}"};
              book.AuthorsLink.AddRange(
              [
                  new BookAuthorLazy() {Book = book, Author = author, Order = 0},
                  new BookAuthorLazy {Book = book, Author = commonAuthor, Order = 1}
              ]);

              result.Add(book);
          }

          return result;
      }
  }