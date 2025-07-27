using BookApp.Extensions;
using BookApp.Persistence;
using BookApp.Services.Api.Books;
using BookApp.Services.Books;
using BookApp.Services.Checkout;
using BookApp.Services.Order;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Регистрируем сервисы в DI
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<ListBooksService>();
builder.Services.AddScoped<CheckoutService>();
builder.Services.AddScoped<DisplayOrdersService>();
builder.Services.AddScoped<IChangePubDateService, ChangePubDateService>();
builder.Services.AddScoped<IChangePriceOfferService, ChangePriceOfferService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
await app.ApplyMigrationsAsync();
app.Run();