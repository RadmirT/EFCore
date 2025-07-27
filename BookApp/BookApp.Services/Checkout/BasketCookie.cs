namespace BookApp.Services.Checkout;

using Microsoft.AspNetCore.Http;

public class BasketCookie(IRequestCookieCollection cookiesIn, IResponseCookies? cookiesOut = null)
    : CookieTemplate(BasketCookieName, cookiesIn, cookiesOut)
{
    private const string BasketCookieName = "bookapp-basket";

    protected override int ExpiresInThisManyDays => 200;  
}
