using Mailjet.Client;
using Microsoft.EntityFrameworkCore;
using MojecHRApp.BusinessManager.AdminLayer;
using MojecHRApp.BusinessManager.StaffLayer;
using MojecHRApp.DAL;
using static MojecHRApp.BusinessManager.MailService.MailJetService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<HRDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
});

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<IStaffServices, StaffServices>();
builder.Services.AddScoped<IAdminServices, AdminServices>();
builder.Services.AddTransient<IMailService, MailjetService>();
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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=Login}/{id?}");

app.Run();
