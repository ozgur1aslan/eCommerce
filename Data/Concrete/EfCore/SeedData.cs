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
        private const string adminUser = "ozgur@admin.com";
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
                    Email = "ozgur@admin.com",
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
                            new Value { ValueText = "Orange"}
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


            
            
            
            

            

            
        }
    }
}