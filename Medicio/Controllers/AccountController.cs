using Medicio.Models;
using Medicio.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medicio.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View(register);
            AppUser user = new AppUser() 
            {
                Fullname = register.Fullname,
                Email = register.Email,
                UserName = register.Username
            };
            var result = await _userManager.CreateAsync(user,register.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            /*var role = _roleManager.CreateAsync(new IdentityRole() { Name = "admin" });*/
            await _userManager.AddToRoleAsync(user, "admin");
            return RedirectToAction("index","home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);
            AppUser user = await _userManager.FindByNameAsync(loginVM.Username);
            if (user is null) return View(loginVM);
            if (!(await _userManager.CheckPasswordAsync(user, loginVM.Password)) ) {
                ModelState.AddModelError("", "Password or username is uncorrect!");
                return View(loginVM);
            }
            await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
            return RedirectToAction("index", "home");
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}
