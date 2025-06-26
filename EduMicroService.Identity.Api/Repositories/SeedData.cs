using EduMicroService.Identity.Api.Features.Roles;
using EduMicroService.Identity.Api.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EduMicroService.Identity.Api.Repositories
{
    public static class SeedData
    {
        public static void EnsureSeedData(this WebApplication app)
        {


            using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!userManager.Users.Any())
            {
                Log.Information("Seeding database...");

                var mustafa = userManager.FindByNameAsync("mustafa").Result;
                if (mustafa != null)
                    Log.Debug("mustafa already exists");

                mustafa = new AppUser
                {
                    Id = NewId.NextSequentialGuid(),
                    UserName = "mustafa",
                    Email = "mustafa@example.com",
                    EmailConfirmed = true,
                };
                var result = userManager.CreateAsync(mustafa, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                var roleExists = roleManager.RoleExistsAsync("Admin").Result;
                if (roleExists)
                {
                    Log.Debug("Admin role already exists");
                }
                else
                {
                    var role = new AppRole
                    {
                        Id = NewId.NextSequentialGuid(),
                        Name = "Admin",
                    };
                    result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }

                roleExists = roleManager.RoleExistsAsync("Customer").Result;
                if (roleExists)
                {
                    Log.Debug("Customer role already exists");
                }
                else
                {
                    var role = new AppRole
                    {
                        Id = NewId.NextSequentialGuid(),
                        Name = "Customer",
                    };
                    result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }

                var isUserInAdminRole = userManager.IsInRoleAsync(mustafa, "Admin").Result;
                if (!isUserInAdminRole)
                {
                    userManager.AddToRoleAsync(mustafa, "Admin").Wait();
                    userManager.AddToRoleAsync(mustafa, "Customer").Wait();

                }

                Log.Information("Done seeding database. Exiting.");

            }

        }
    }
}
