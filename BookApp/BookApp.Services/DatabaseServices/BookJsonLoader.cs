using System.Text.Json;
using BookApp.Entities;

namespace BookApp.Services.DatabaseServices;

public static class BookJsonLoader
{
    public static IEnumerable<Book> LoadBooks(string fileDir, string fileSearchString)
    {
        var filePath = GetJsonFilePath(fileDir, fileSearchString);
        var jsonDecoded = JsonSerializer.Deserialize<ICollection<BookInfoJson>>(File.ReadAllText(filePath));

        var authorDict = new Dictionary<string, Author>();
        var tagDict = new Dictionary<string, Tag>();
        foreach (var bookInfoJson in jsonDecoded)
        {
            foreach (var author in bookInfoJson.authors)
            {
                if (!authorDict.ContainsKey(author))
                    authorDict[author] = new Author { Name = author };
            }

            foreach (var category in bookInfoJson.categories)
            {
                if (!tagDict.ContainsKey(category))
                    tagDict[category] = new Tag { TagId = category };
            }

        }

        return jsonDecoded.Select(x => CreateBookWithRefs(x, authorDict, tagDict));
    }


    private static Book CreateBookWithRefs(BookInfoJson bookInfoJson,
        Dictionary<string, Author> authorDict,
        Dictionary<string, Tag> tagsDict)
    {
        var book = new Book
        {
            Title = bookInfoJson.title,
            Description = bookInfoJson.description,
            PublishedOn = DecodePubishDate(bookInfoJson.publishedDate),
            Publisher = bookInfoJson.publisher,
            Price = (decimal)(bookInfoJson.saleInfoListPriceAmount ?? -1),
            ImageUrl = bookInfoJson.imageLinksThumbnail
        };

        byte i = 0;
        foreach (var author in bookInfoJson.authors)
        {
            book.AuthorsLink.Add(new BookAuthor { Book = book, Author = authorDict[author], Order = i++ });
        }

        foreach (var category in bookInfoJson.categories)
        {
            book.Tags.Add(tagsDict[category]);
        }

        if (bookInfoJson.averageRating != null)
        {
            foreach (var review in CalculateReviewsToMatch((double)bookInfoJson.averageRating, (int)bookInfoJson.ratingsCount))
            {
                book.Reviews.Add(review);
            }
        }

        return book;
    }

    internal static IList<Review> CalculateReviewsToMatch(double averageRating, int ratingsCount)
    {
        var reviews = new List<Review>();
        var currentAve = averageRating;
        for (int i = 0; i < ratingsCount; i++)
        {
            reviews.Add(new Review
            {
                VoterName = "anonymous",
                NumStars = (int)(currentAve > averageRating
                    ? Math.Truncate(averageRating)
                    : Math.Ceiling(averageRating))
            });
            currentAve = reviews.Average(x => x.NumStars);
        }

        return reviews;
    }

    private static DateOnly DecodePubishDate(string publishedDate)
    {
        var split = publishedDate.Split('-');
        switch (split.Length)
        {
            case 1:
                return new DateOnly(int.Parse(split[0]), 1, 1);
            case 2:
                return new DateOnly(int.Parse(split[0]), int.Parse(split[1]), 1);
            case 3:
                return new DateOnly(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
        }

        throw new InvalidOperationException($"The json publishedDate failed to decode: string was {publishedDate}");
    }

    private static string GetJsonFilePath(string fileDir, string searchPattern)
    {
        var fileList = Directory.GetFiles(fileDir, searchPattern);

        if (fileList.Length == 0)
            throw new FileNotFoundException(
                $"Could not find a file with the search name of {searchPattern} in directory {fileDir}");

        return fileList.ToList().OrderBy(x => x).Last();
    }
}