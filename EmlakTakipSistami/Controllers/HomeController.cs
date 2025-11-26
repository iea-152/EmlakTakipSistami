using EmlakTakipSistami.Models;
using EmlakTakipSistami.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EmlakTakipSistami.Controllers
{
    // Sadece giriş yapanlar görebilsin
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // 1. İstatistikleri Hesapla

            // Toplam Daire Sayısı
            int toplamDaire = _context.Daireler.Count();

            // Toplam Kiracı Sayısı
            int toplamKiraci = _context.Kiracilar.Count();

            // Boş Daire Sayısı (Toplam - Dolu Olanlar)
            // Not: Basitçe Kiracı sayısı kadar daire dolu varsayıyoruz.
            int bosDaire = toplamDaire - toplamKiraci;
            if (bosDaire < 0) bosDaire = 0;

            // Toplam Aylık Kira Geliri (Sadece kiracısı olan dairelerin kirasını topla)
            // Kiracılar tablosundaki DaireId'leri alıp, Daireler tablosunda o ID'lerin kira ücretlerini topluyoruz.
            decimal toplamGelir = 0;
            var kiradakiDaireIds = _context.Kiracilar.Select(k => k.DaireId).ToList();
            if (kiradakiDaireIds.Any())
            {
                toplamGelir = _context.Daireler
                    .Where(d => kiradakiDaireIds.Contains(d.Id))
                    .Sum(d => d.KiraUcreti);
            }

            // 2. Verileri ViewBag ile Sayfaya Taşı
            ViewBag.ToplamDaire = toplamDaire;
            ViewBag.ToplamKiraci = toplamKiraci;
            ViewBag.BosDaire = bosDaire;
            ViewBag.ToplamGelir = toplamGelir;

            // 3. Son 5 Aktivite (Son eklenen 5 kiracı)
            var sonKiracilar = _context.Kiracilar
                .Include(k => k.Daire)
                .OrderByDescending(k => k.Id)
                .Take(5)
                .ToList();

            return View(sonKiracilar);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}