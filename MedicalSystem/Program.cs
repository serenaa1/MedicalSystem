using MedicalSystem.Interfaces.Abstract;
using MedicalSystem.Models;
using MedicalSystem.Models.Domain;
using MedicalSystem.Repositories.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

//for identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
    AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(op => op.LoginPath = "/Login/Login");

builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

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
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
