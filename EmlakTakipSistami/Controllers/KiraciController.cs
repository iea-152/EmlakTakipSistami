using EmlakTakipSistami.Models;
using EmlakTakipSistami.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class KiraciController : Controller
{
    private readonly ApplicationDbContext _context;

    public KiraciController(ApplicationDbContext context)
    {
        _context = context;
    }

 public IActionResult Index()
    {
        var kiracilar = _context.Kiracilar
            .Include(k => k.Daire) // Daire bilgisi de gelsin
            .ToList();
        return View(kiracilar);
    }

    // Ekleme (GET)
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Daireler = _context.Daireler.ToList() ?? new List<Daire>();
        return View();
    }

    // Ekleme (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Kiraci kiraci)
    {
        if (ModelState.IsValid)
        {
            _context.Kiracilar.Add(kiraci);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.Daireler = _context.Daireler.ToList();
        return View(kiraci);
    }


    // Düzenleme (GET)
    public IActionResult Edit(int id)
    {
        var kiraci = _context.Kiracilar.Find(id);
        if (kiraci == null) return NotFound();

        PrepareDaireDropdown(kiraci.DaireId);
        return View(kiraci);
    }

    // Düzenleme (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Kiraci kiraci)
    {
        if (ModelState.IsValid)
        {
            _context.Kiracilar.Update(kiraci);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        PrepareDaireDropdown(kiraci.DaireId);
        return View(kiraci);
    }

    // Helper method: Daire dropdown hazırla
    private void PrepareDaireDropdown(int? selectedId = null)
    {
        ViewBag.Daireler = _context.Daireler
            .Select(d => new
            {
                d.Id,
                // Artık Daire Adı varsa onu, yoksa İl/İlçe bilgisini gösterir
                DisplayName = (d.DaireAdi != null ? d.DaireAdi + " - " : "") + d.Il + "/" + d.Ilce + " (" + d.KiraUcreti + "₺)"
            })
            .ToList();

        ViewBag.SelectedDaireId = selectedId;
    }
}
