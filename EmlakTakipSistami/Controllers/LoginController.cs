using EmlakTakipSistami.Models;
using EmlakTakipSistami.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmlakTakipSistami.Controllers
{
    public class LoginController : Controller
    {
        // VERİTABANI BAĞLANTISI BURADA TANIMLANIYOR
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Giriş Sayfası (GET)
        [HttpGet]
        public IActionResult GirisYap()
        {
            // Eğer zaten giriş yapılmışsa Ana Sayfaya yönlendir
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // Giriş Yapma İşlemi (POST)
        [HttpPost]
        public async Task<IActionResult> GirisYap(string kadi, string sifre)
        {
            // 1. Veritabanında kullanıcıyı ara
            var user = _context.Users.FirstOrDefault(x => x.KullaniciAdi == kadi && x.Sifre == sifre);

            if (user != null)
            {
                // 2. Kullanıcı bulundu, sisteme tanıt (Cookie oluştur)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Ad + " " + user.Soyad),
                    new Claim(ClaimTypes.Role, user.Rol)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }

            // Hatalı giriş
            ViewBag.Hata = "Kullanıcı adı veya şifre yanlış!";
            return View();
        }

        // Çıkış Yapma
        public async Task<IActionResult> CikisYap()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("GirisYap");
        }

        // Admin Oluşturucu (Tek seferlik çalıştırılacak)
        [HttpGet]
        public IActionResult AdminOlustur()
        {
            var adminVarMi = _context.Users.Any(x => x.KullaniciAdi == "admin");

            if (!adminVarMi)
            {
                var user = new Users
                {
                    Ad = "Admin",
                    Soyad = "Yönetici",
                    KullaniciAdi = "admin",
                    Email = "admin@sistem.com",
                    Sifre = "123",
                    Rol = "Admin",
                    AktifMi = true,
                    KayitTarihi = DateTime.Now
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return Content("Başarılı! Admin kullanıcısı oluşturuldu.\nKullanıcı Adı: admin\nŞifre: 123");
            }

            return Content("Zaten bir admin kullanıcısı mevcut. Giriş sayfasına dönebilirsiniz.");
        }
    }
}