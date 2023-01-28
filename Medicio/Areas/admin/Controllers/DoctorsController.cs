using Medicio.DAL;
using Medicio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicio.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "admin")]
    public class DoctorsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DoctorsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var doctors = _context.Doctors.ToList();
            return View(doctors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            if (!ModelState.IsValid) return View(doctor);
            if (doctor.FormFile is not null)
            {
                string imageName = Guid.NewGuid() + doctor.FormFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets/img/doctors", imageName);
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    doctor.FormFile.CopyTo(fileStream);
                }
                doctor.Image = imageName;
                await _context.Doctors.AddAsync(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }
        public IActionResult Edit(int id)
        {
            var existDoctor = _context.Doctors.Find(id);
            if (existDoctor is null) return RedirectToAction(nameof(Index));
            return View(existDoctor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Doctor doctor)
        {
            var existDoctor = _context.Doctors.Find(doctor.Id);
            if (doctor.FormFile is not null)
            {
                string imageName = Guid.NewGuid() + doctor.FormFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets/img/doctors", imageName);
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    doctor.FormFile.CopyTo(fileStream);
                }
                existDoctor.Image = imageName;
                existDoctor.Position = doctor.Position;
                existDoctor.Fullname = doctor.Fullname;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }
        public IActionResult Delete(int id)
        {
            var existDoctor = _context.Doctors.Find(id);
            if (existDoctor is null) return RedirectToAction(nameof(Index));
            _context.Doctors.Remove(existDoctor);   
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
