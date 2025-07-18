using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.IdentityModuleDto;

namespace AdminDashBoard.Controllers
{

    public class AdminController(SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> _userManager) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Invalid Email");
                return RedirectToAction(nameof(Login));
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (result.Succeeded && await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction(nameof(Index),"Home");
            }
            ModelState.AddModelError(string.Empty, "You are not authorized");
            return View(login);

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
