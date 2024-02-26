using eCommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Concrete.EfCore
{
    public class eCommerceContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public eCommerceContext(DbContextOptions<eCommerceContext> options) : base(options){
            
        }
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Season> Seasons => Set<Season>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Gender> Genders => Set<Gender>();
        public DbSet<Tag> Tags => Set<Tag>();


        public DbSet<UserGender> UserGenders => Set<UserGender>();

        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PurchasedItem> PurchasedItems { get; set; }


        public DbSet<Product> Products => Set<Product>();
        public DbSet<Variant> Variants => Set<Variant>();
        public DbSet<Picture> Pictures => Set<Picture>();
        public DbSet<Option> Options => Set<Option>();
        public DbSet<Value> Values => Set<Value>();


        


        
        
    }
}