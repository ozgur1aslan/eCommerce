using eCommerce.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace eCommerce.Data.Concrete.EfCore
{
    public static class SeedData
    {
        private const string adminUser = "user@admin.com";
        private const string adminPassword = "admin123";

        public static async void SeedNeededData(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateAsyncScope().ServiceProvider.GetRequiredService<eCommerceContext>();

            if (context.Database.GetAppliedMigrations().Any())
            {
                context.Database.Migrate();
            }

            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

            // Ensure Admin role exists
            var adminRole = await roleManager.FindByNameAsync("Admin");

            if (adminRole == null)
            {
                await roleManager.CreateAsync(new AppRole { Name = "Admin" });
            }

            var user = await userManager.FindByNameAsync(adminUser);

            if (user == null)
            {
                user = new AppUser
                {
                    FullName = "Özgür Aslan",
                    UserName = adminUser,
                    Email = "user@admin.com",
                    PhoneNumber = "44554455445"
                };

                await userManager.CreateAsync(user, adminPassword);

                // Assign the Admin role to the user
                await userManager.AddToRoleAsync(user, "Admin");
            }


            if(!context.Genders.Any())
            {
                context.Genders.AddRange(
                    new Gender { GenderName = "Male"},
                    new Gender { GenderName = "Female"},
                    new Gender { GenderName = "Unisex"}
                );
                context.SaveChanges();
            }

            if(!context.Seasons.Any())
            {
                context.Seasons.AddRange(
                    new Season { SeasonName = "Winter"},
                    new Season { SeasonName = "Spring"},
                    new Season { SeasonName = "Summer"},
                    new Season { SeasonName = "Fall"}
                );
                context.SaveChanges();
            }

            if(!context.Options.Any())
            {
                context.Options.AddRange(
                    new Option { 
                        OptionName = "Size",
                        Values = new List<Value> {
                            new Value { ValueText = "S"},
                            new Value { ValueText = "M"},
                            new Value { ValueText = "L"},
                            new Value { ValueText = "XL"},
                            }
                        },
                    new Option { 
                        OptionName = "Color",
                        Values = new List<Value> {
                            new Value { ValueText = "Red"},
                            new Value { ValueText = "Blue"},
                            new Value { ValueText = "Black"},
                            new Value { ValueText = "White"},
                            new Value { ValueText = "Orange"},
                            new Value { ValueText = "Green"},
                            new Value { ValueText = "Yellow"},
                            new Value { ValueText = "Grey"},
                            new Value { ValueText = "Purple"},
                            new Value { ValueText = "Pink"},
                            }
                        }
                );

                context.SaveChanges();

            }


            if(!context.UserGenders.Any())
            {
                context.UserGenders.AddRange(
                    new UserGender { UserGenderName = "Male"},
                    new UserGender { UserGenderName = "Female"},
                    new UserGender { UserGenderName = "Custom"},
                    new UserGender { UserGenderName = "Rather not say"}
                );

                context.SaveChanges();
            }

            if(!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { CategoryName = "T-Shirt"},
                    new Category { CategoryName = "Sweatshirt"},
                    new Category { CategoryName = "Sweater"},
                    new Category { CategoryName = "Sweatshirt"},
                    new Category { CategoryName = "Shirt"},
                    new Category { CategoryName = "Watch"},
                    new Category { CategoryName = "Glasses"},
                    new Category { CategoryName = "Jeans"},
                    new Category { CategoryName = "Collar"},
                    new Category { CategoryName = "Top"},
                    new Category { CategoryName = "Gloves"},
                    new Category { CategoryName = "Coat"},
                    new Category { CategoryName = "Pants"},
                    new Category { CategoryName = "Wallet"},
                    new Category { CategoryName = "Scarf"},
                    new Category { CategoryName = "Jewellery"},
                    new Category { CategoryName = "Bra"},
                    new Category { CategoryName = "Ring"},
                    new Category { CategoryName = "Purse"},
                    new Category { CategoryName = "Lipstick"},
                    new Category { CategoryName = "Vest"},
                    new Category { CategoryName = "Tie"},
                    new Category { CategoryName = "Handbag"},
                    new Category { CategoryName = "Earring"},
                    new Category { CategoryName = "Cycling Shorts"}
                );

                context.SaveChanges();
            }


            if(!context.Brands.Any())
            {
                context.Brands.AddRange(
                    new Brand { BrandName = "Nike"},
                    new Brand { BrandName = "Adidas"},
                    new Brand { BrandName = "Rolex"},
                    new Brand { BrandName = "Calvin Klein"},
                    new Brand { BrandName = "Gucci"},
                    new Brand { BrandName = "Hermes"},
                    new Brand { BrandName = "Dior"},
                    new Brand { BrandName = "Burberry"},
                    new Brand { BrandName = "Puma"},
                    new Brand { BrandName = "Prada"},
                    new Brand { BrandName = "Rayban"},
                    new Brand { BrandName = "Zara"},
                    new Brand { BrandName = "Anta"},
                    new Brand { BrandName = "Moncler"},
                    new Brand { BrandName = "Pandora"},
                    new Brand { BrandName = "Fila"},
                    new Brand { BrandName = "Celine"},
                    new Brand { BrandName = "Ted Baker"},
                    new Brand { BrandName = "Cavalli"},
                    new Brand { BrandName = "Swatch"},
                    new Brand { BrandName = "Desigual"},
                    new Brand { BrandName = "Banana Republic"},
                    new Brand { BrandName = "Tag Heuer"},
                    new Brand { BrandName = "ESCADA"},
                    new Brand { BrandName = "GAP"}

                );

                context.SaveChanges();
            }
            
            
        }
    }
}