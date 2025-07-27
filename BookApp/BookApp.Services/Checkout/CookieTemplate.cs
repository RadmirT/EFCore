namespace BookApp.Services.Checkout;

using System;
using Microsoft.AspNetCore.Http;

public abstract class CookieTemplate
{

    private readonly IRequestCookieCollection _cookiesIn;
    private readonly IResponseCookies? _cookiesOut;  
    private readonly string _cookieName;

    protected CookieTemplate(string cookieName, IRequestCookieCollection? cookiesIn, IResponseCookies? cookiesOut = null)
    {
        _cookiesIn = cookiesIn ?? throw new ArgumentNullException(nameof(cookiesIn));
        _cookiesOut = cookiesOut;
        _cookieName = cookieName;
    }

    /// <summary>
    /// Количество дней, в течении которых будет действовать куки. Если 0 - куки будут действовать до истечения сессии.
    /// </summary>
    protected virtual int ExpiresInThisManyDays => 0;

    public void AddOrUpdateCookie(string value)
    {
        if (_cookiesOut == null)
        {
            throw new NullReferenceException(
                "You must supply a IResponseCookies value if you want to use this command.");
        }

        var options = new CookieOptions();
        if (ExpiresInThisManyDays > 0)
        {
            options.Expires = DateTime.Now.AddDays(ExpiresInThisManyDays);
        }

        _cookiesOut.Append(_cookieName, value, options);
    }

    public bool Exists()
    {
        return _cookiesIn[_cookieName] != null;
    }

    public string? GetValue()
    {
        var cookie = _cookiesIn[_cookieName];
        return string.IsNullOrEmpty(cookie) ? null : cookie;
    }

    public void DeleteCookie()
    {
        if (_cookiesOut == null)
            throw new NullReferenceException("You must supply a IResponseCookies value if you want to use this command.");

        if (!Exists()) return;
        var options = new CookieOptions {Expires = DateTime.Now.AddYears(-1)};
        _cookiesOut.Append(_cookieName, "", options);
    }
}