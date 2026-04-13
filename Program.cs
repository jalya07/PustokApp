using Microsoft.EntityFrameworkCore;
using pustokApp.Data;
using pustokApp.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<BankService>();

var confg = builder.Configuration;
builder.Services.AddDbContext<PustokAppDbContext>(options =>
    options.UseSqlServer(confg.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();