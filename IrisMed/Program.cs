using IrisMed.Areas.Identity.Data;
using IrisMed.Data;
using IrisMed.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IrisMed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<AppointmentsContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<InventoryContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ContactUsContext>(options => 
options.UseSqlServer(connectionString));
builder.Services.AddDbContext<DataShiftContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<StaffBoardContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<LogsContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IrisUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddDbContext<ContactUsContext>(options => options.UseSqlServer());
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
