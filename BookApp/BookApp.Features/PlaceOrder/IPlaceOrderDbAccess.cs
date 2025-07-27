namespace BookApp.Features.PlaceOrder;

using BookApp.Entities;

public interface IPlaceOrderDbAccess
{
    IEnumerable<Book> GetBooksWithPriceOffers(IEnumerable<int> booksIds);
    void AddOrder(Order order);
}