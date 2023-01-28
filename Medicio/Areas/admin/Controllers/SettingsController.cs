using Medicio.DAL;
using Medicio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicio.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "admin")]

    public class SettingsController : Controller
    {
        
        private readonly AppDbContext _context;

        public SettingsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var settings = _context.Settings.ToList();
            return View(settings);
        }
        public IActionResult Edit(int id)
        {
            var setting = _context.Settings.Find(id);
            if (setting is null) return RedirectToAction(nameof(Index));
            return View(setting);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Setting setting)
        {
            var existSetting = _context.Settings.Find(setting.Id);
            if (existSetting is null) return RedirectToAction(nameof(Index));
            existSetting.NameOfDoctorsSection = setting.NameOfDoctorsSection;
            existSetting.TitleOfDoctorsSection = setting.TitleOfDoctorsSection;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
