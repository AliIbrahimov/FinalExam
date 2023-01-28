using Medicio.DAL;
using Medicio.Models;
using Medicio.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicio.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        HomeVM vm = new HomeVM()
        {
            Doctors = _context.Doctors.Include(x=>x.Icons).ToList(),
            Settings = _context.Settings.ToList()
        };
        return View(vm);
    }

}