using EmlakTakipSistami.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies; // Bu kütüphane gerekli

var builder = WebApplication.CreateBuilder(args);

// 1. Servislerin Eklendiði Bölüm
builder.Services.AddControllersWithViews();

// Veritabaný Baðlantýsý
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Authentication (Giriþ) Ayarlarýný Startup.cs'ten buraya taþýdýk
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/GirisYap/"; // Giriþ yapýlmamýþsa buraya atar
    });

var app = builder.Build();

// 2. HTTP Ýstek Hattý (Pipeline) Ayarlarý
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Önce kimlik doðrulama (Authentication), sonra yetkilendirme (Authorization) olmalý
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();