using Autofac;
using Autofac.Extensions.DependencyInjection;
using Salam.Inventory.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Salam.Inventory.Web;
using System.Reflection;
using Serilog;

#region Configure Bootstrap Logger using serilog 
var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateBootstrapLogger();
#endregion


try
{
    Log.Information("Application Starting.....");

    var builder = WebApplication.CreateBuilder(args);

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    var migrationAssembly = Assembly.GetExecutingAssembly().FullName;

    #region Serilog integration for Application logs
    builder.Host.UseSerilog((services, ls) => ls
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(builder.Configuration));
    #endregion


    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddIdentity();
    builder.Services.AddControllersWithViews();

    #region Autofac Configuration For Dependency Injection
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(
        containerBuilder =>
        {
            containerBuilder.RegisterModule(new WebModule(connectionString, migrationAssembly));
        }));
    #endregion

    #region AutoMapper Configuration
    builder.Services.AddAutoMapper(typeof(WebProfile).Assembly);
    #endregion

    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
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

    app.MapRazorPages()
        .WithStaticAssets();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Fatal Error occurred while starting the application...");
}
finally
{
    Log.CloseAndFlush();
}