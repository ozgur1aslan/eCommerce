using eCommerce.Data;
using eCommerce.Data.Abstract;
using eCommerce.Data.Concrete;
using eCommerce.Data.Concrete.EfCore;
using eCommerce.Models;
using eCommerce.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<eCommerceContext>(options => {
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("sql_connection");
    options.UseSqlite(connectionString);
});


builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<eCommerceContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

    options.User.RequireUniqueEmail = true;
    // options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz";

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;

    options.SignIn.RequireConfirmedEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>{
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/AccesDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});



builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddScoped<IVariantRepository, EfVariantRepository>();
builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();
builder.Services.AddScoped<ICategoryRepository, EfCategoryRepository>();
builder.Services.AddScoped<ISeasonRepository, EfSeasonRepository>();
builder.Services.AddScoped<IBrandRepository, EfBrandRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();

builder.Services.AddScoped<IWishlistRepository, EfWishlistRepository>();
builder.Services.AddScoped<ICartRepository, EfCartRepository>();
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();


builder.Services.AddScoped<EmailService>();

// Add WishlistService registration
builder.Services.AddScoped<WishlistService>();
builder.Services.AddScoped<CartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

SeedData.SeedNeededData(app);



app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "User",
    pattern: "{area:exists}/{controller=Wishlist}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();