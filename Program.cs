using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PresentroMVC.Models;
using PresentroMVC.PresentroDataContext;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PresentroDbContext>( opt=>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    });

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
    {
        opt.Password.RequiredLength = 8;
        opt.Password.RequireDigit = true;
        opt.Password.RequireNonAlphanumeric = false;
        opt.SignIn.RequireConfirmedEmail = false;
        opt.User.RequireUniqueEmail = false;
    })
    .AddEntityFrameworkStores<PresentroDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
