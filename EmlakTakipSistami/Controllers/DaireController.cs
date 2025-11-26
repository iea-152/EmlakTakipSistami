using EmlakTakipSistami.Models;
using EmlakTakipSistami.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmlakTakipSistami.Controllers
{
    public class DaireController : Controller
    {


        private readonly ApplicationDbContext _context;

        public DaireController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Listeleme
        public IActionResult Index()
        {
            var daireler = _context.Daireler.ToList();
            return View(daireler);
        }

        // Detay
        public IActionResult Details(int id)
        {
            var daire = _context.Daireler.Find(id);
            if (daire == null) return NotFound();
            return View(daire);
        }

        // Ekleme (GET)
        [HttpGet]
     
        public IActionResult Create()
        {
            string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "iller.json");

            if (!System.IO.File.Exists(jsonPath))
            {
                ViewBag.Iller = new List<string>();
                ViewBag.IlcelerJson = "{}";
                return View(new Daire());
            }

            var jsonContent = System.IO.File.ReadAllText(jsonPath);

            var ilIlceDict = System.Text.Json.JsonSerializer
                .Deserialize<Dictionary<string, List<string>>>(jsonContent);

            ViewBag.Iller = ilIlceDict?.Keys.ToList() ?? new List<string>();
            ViewBag.IlcelerJson = System.Text.Json.JsonSerializer.Serialize(ilIlceDict ?? new Dictionary<string, List<string>>());

            return View(new Daire());
        }







        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Daire daire)
        {
            if (ModelState.IsValid)
            {
                _context.Daireler.Add(daire);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Eğer model geçerli değilse tekrar formu göster
            string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "iller.json");
            var jsonContent = System.IO.File.ReadAllText(jsonPath);
            var ilIlceDict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonContent);
            ViewBag.Iller = ilIlceDict?.Keys.ToList() ?? new List<string>();
            ViewBag.IlcelerJson = System.Text.Json.JsonSerializer.Serialize(ilIlceDict ?? new Dictionary<string, List<string>>());

            return View(daire);
        }


        // Güncelleme (GET)
        public IActionResult Edit(int id)
        {
            var daire = _context.Daireler.Find(id);
            if (daire == null) return NotFound();

            // İller ve ilçeler JSON
            string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "iller.json");
            var jsonContent = System.IO.File.ReadAllText(jsonPath);
            var ilIlceDict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonContent);

            ViewBag.Iller = ilIlceDict?.Keys.ToList() ?? new List<string>();
            ViewBag.IlcelerJson = System.Text.Json.JsonSerializer.Serialize(ilIlceDict ?? new Dictionary<string, List<string>>());

            return View(daire);
        }


        // Güncelleme (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Daire daire)
        {
            if (ModelState.IsValid)
            {
                _context.Daireler.Update(daire);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Model geçersizse dropdownları tekrar doldur
            string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "iller.json");
            var jsonContent = System.IO.File.ReadAllText(jsonPath);
            var ilIlceDict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonContent);

            ViewBag.Iller = ilIlceDict?.Keys.ToList() ?? new List<string>();
            ViewBag.IlcelerJson = System.Text.Json.JsonSerializer.Serialize(ilIlceDict ?? new Dictionary<string, List<string>>());

            return View(daire);
        }


        // Silme
        public IActionResult Delete(int id)
        {
            var daire = _context.Daireler.Find(id);
            if (daire == null) return NotFound();

            _context.Daireler.Remove(daire);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
