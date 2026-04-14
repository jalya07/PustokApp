using Microsoft.EntityFrameworkCore;
using pustokApp.Data;
using pustokApp.Service;
using pustokApp.Setting;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<BankService>();
builder.Services.AddScoped<BankManager>();
builder.Services.AddScoped<LayoutService>();
builder.Services.Configure<GroupInfoSettings>(config.GetSection("GroupInfo"));

// var confg = builder.Configuration;
builder.Services.AddDbContext<PustokAppDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name : "areas",
    pattern : "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();