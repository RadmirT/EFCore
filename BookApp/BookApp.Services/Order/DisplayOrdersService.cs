namespace BookApp.Services.Order;

using System;
using System.Collections.Generic;
using System.Linq;
using BookApp.Entities;
using BookApp.Persistence;
using BookApp.Services.Api.Orders.Contracts;

public class DisplayOrdersService(AppDbContext context)
{
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    
    public List<OrderResponse> GetUsersOrders()
    {

        return SelectQuery(_context.Orders.OrderByDescending(x => x.DateOrderedUtc))
            .ToList();
    }
    
    public OrderResponse GetOrderDetail(int orderId)
    {
        var order = SelectQuery(_context.Orders).SingleOrDefault(x => x.OrderId == orderId);

        if (order == null)
            throw new NullReferenceException($"Could not find the order with id of {orderId}.");

        return order;
    }
    
    private IQueryable<OrderResponse> SelectQuery(IQueryable<Order> orders)
    {
        return orders.Select(x => new OrderResponse
        {
            OrderId = x.OrderId,
            DateOrderedUtc = x.DateOrderedUtc,
            LineItems = x.LineItems.Select(lineItem => new OrderLine(
                lineItem.ChosenBook.BookId,
                lineItem.ChosenBook.Title,
                string.Join(", ",
                    lineItem.ChosenBook.AuthorsLink
                        .OrderBy(q => q.Order)
                        .Select(q => q.Author.Name)),
                lineItem.BookPrice,
                lineItem.ChosenBook.ImageUrl,
                lineItem.NumBooks))
        });
    }
}