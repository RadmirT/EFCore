namespace BookApp.Services.Checkout;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookApp.Services.Api.Orders.Contracts;

public class CookieBasket : IBasket
{
    private readonly BasketCookie _cookies;
    private readonly Dictionary<int,BasketLineRequest> _lineItems = new();

    public CookieBasket(BasketCookie cookies)
    {
        this._cookies = cookies;
        this.DecodeCookieString(cookies.GetValue());
    }
    private void DecodeCookieString(string? cookieContent)
    {
        if (cookieContent == null)
        {
            return;
        }

        var parts = cookieContent.Split(',');
        for (var i = 0; i < (parts.Length) / 2; i++)
        {
            var bookId = int.Parse(parts[i * 2]);
            this._lineItems[bookId] = new BasketLineRequest()
            {
                BookId = bookId,
                Quantity = ushort.Parse(parts[i * 2 + 1])
            };
        }
    }


    public Guid UserId { get; private set;} = Guid.Parse("abbe04d5-2b2a-4b7d-a1a8-6ccd8ce4d5f2");
    public IReadOnlyList<BasketLineRequest> LineItems => this._lineItems.Values.ToList().AsReadOnly();

    public void Save()                  
    {                                                
        var sb = new StringBuilder();                
        foreach (var lineItem in this.LineItems)         
        {
            if (sb.Length > 0)
            {
                sb.Append(',');
            }
            sb.AppendFormat("{0},{1}", lineItem.BookId, lineItem.Quantity); 
        }                                            
        this._cookies.AddOrUpdateCookie(sb.ToString());
    }

    public void Clear()
    {
        this._lineItems.Clear();
    }

    public void Add(int bookId, ushort quantity)
    {
        if (this._lineItems.TryGetValue(bookId, out var item))
        {
            item.Quantity += quantity;
        }
        else
        {
            this._lineItems[bookId] = new BasketLineRequest()

            {
                BookId = bookId,
                Quantity = quantity,
            };
        }
    }
}