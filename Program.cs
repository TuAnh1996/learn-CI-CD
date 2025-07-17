using Microsoft.EntityFrameworkCore;

using UserManagerDemo.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Thêm dịch vụ DbContext và kết nối SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();
    
// add DB context 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


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
    pattern: "{controller=Home}/{action=Index}/{id?}"); // HÃY BỎ COMMENT DÒNG NÀY

// Route cụ thể của bạn cho Student/Create
app.MapControllerRoute(
    name: "studentCreate",
    pattern: "student/create",
    defaults: new { controller = "Student", action = "Create" }
);
// Nếu bạn dùng Razor Pages (và Identity UI là Razor Pages):
app.MapRazorPages(); // <<<<<<<<<< ĐẢM BẢO CÓ DÒNG NÀY
app.Run();
