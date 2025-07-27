namespace BookApp.Services.Order;

using System;
using System.Collections.Generic;
using System.Linq;
using BookApp.Entities;
using BookApp.Features.PlaceOrder;
using BookApp.Persistence;
using Microsoft.EntityFrameworkCore;

public class PlaceOrderDbAccess (AppDbContext dbContext): IPlaceOrderDbAccess
{
    private readonly AppDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public IEnumerable<Book> GetBooksWithPriceOffers(IEnumerable<int> booksIds)
    {
        return this._dbContext.Books
            .Where(book => booksIds.Contains(book.BookId))
            .Include(book => book.Promotion).ToList();
    }

    public void AddOrder(Order order)
    {
        this._dbContext.Orders.Add(order);
    }
}