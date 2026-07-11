using CAS.CASDbContext;
using CAS.DTOs.Auth;
using CAS.Identity;
using CAS.Implementation.Repositories;
using CAS.Implementation.Services;
using CAS.Interfaces.Repositories;
using CAS.Interfaces.Services;
using CAS.Models.Contracts.Identity;
using CAS.Models.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Database
builder.Services.AddDbContext<CASContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CASConnection")
    ));

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>(); 
builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterFarmerRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(config =>
  {
      config.LoginPath = "/Auth/login";
      config.Cookie.Name = "CAS";
      config.LogoutPath = "/Auth/logout";
      config.ExpireTimeSpan = TimeSpan.FromMinutes(15);
      config.SlidingExpiration = true;
  });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
