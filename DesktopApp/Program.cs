using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DesktopApp.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Players");
    options.Conventions.AuthorizeFolder("/Coaches", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Teams", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Feedbacks");
    options.Conventions.AuthorizeFolder("/TrainingSessions");
});

builder.Services.AddDbContext<DesktopAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DesktopAppContext") ?? throw new InvalidOperationException("Connection string 'DesktopAppContext' not found.")));

builder.Services.AddDbContext<DesktopAppIdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DesktopAppContext") ?? throw new InvalidOperationException("Connectionstring 'DesktopAppContext' not found.")));
builder.Services.AddDefaultIdentity<IdentityUser>(options =>options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DesktopAppIdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
