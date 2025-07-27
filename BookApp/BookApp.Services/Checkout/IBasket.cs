namespace BookApp.Services.Checkout;

using System;
using System.Collections.Generic;
using BookApp.Services.Api.Orders.Contracts;

public interface IBasket
{
    Guid UserId { get; }
    IReadOnlyList<BasketLineRequest> LineItems { get; }
    void Add(int bookId, ushort quantity);
    void Save();
    void Clear();
}