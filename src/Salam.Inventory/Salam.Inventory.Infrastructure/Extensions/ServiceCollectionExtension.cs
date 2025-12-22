using Salam.Inventory.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Salam.Inventory.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services
               .AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddUserManager<ApplicationUserManager>()
               .AddRoleManager<ApplicationRoleManager>()
               .AddSignInManager<ApplicationSignInManager>()
               .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 4;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz0123456789_";
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministrationPolicy", policy =>
                    policy.RequireRole("Admin"));
                options.AddPolicy("ViewIndexPolicy", policy =>
                    policy.RequireClaim("Can View List?", "true"));
                options.AddPolicy("CreatePolicy", policy =>
                    policy.RequireClaim("Can Create?", "true"));
                options.AddPolicy("EditPolicy", policy =>
                    policy.RequireClaim("Can Edit?", "true"));
                options.AddPolicy("DeletePolicy", policy =>
                    policy.RequireClaim("Can Delete?", "true"));
                options.AddPolicy("DeleteAllPolicy", policy =>
                    policy.RequireClaim("Can Delete All?", "true"));
            });
        }
    }
}