




using MovieApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using FluentAssertions.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;




void RegisterServices(IServiceCollection services)
{
    DependencyInjection.RegisterServices(services);
}





var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole(); // Add Console logger
builder.Logging.AddDebug();   // Add Debug logger


RegisterServices(builder.Services);


// Add SendGrid


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Movie_appContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Movie_appContext") ?? throw new InvalidOperationException("Connection string 'Movie_appContext' not found.")));

//string connectionString = Configuration.GetConnectionString("default");
//string connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddDbContext<AppDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("default") ?? throw new InvalidOperationException("Connection string 'AppDBContext' not found.")));
builder.Services.AddApplicationInsightsTelemetry();



builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDBContext>()
    .AddDefaultTokenProviders();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope()) 
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[]{ "Admin", "Manager", "Member" };


    foreach ( var role in roles)
    { 
    
    if(!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));

    
    }
}


using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string email = "admin@gmail.com";
    string password = "Pass2*";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser();
        user.UserName = email;
        user.Email = email;
        //   user.EmailConfirmed = true;




        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Admin");
    }
}
app.Run();
