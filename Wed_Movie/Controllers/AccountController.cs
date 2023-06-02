using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wed_Movie.DAO;
using Wed_Movie.Data;

namespace Wed_Movie.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Login(string? returnUrl)
        {
            ClaimsPrincipal claims = HttpContext.User;
            if (claims.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginDAO { returnUrl = returnUrl });
        }
        public async Task<IActionResult> Register()
        {
            ClaimsPrincipal claims = HttpContext.User;
            if (claims.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            RegisterDAO model = new RegisterDAO();

            model.externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            model.returnUrl = "";

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDAO model)
        {
            model.returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.email,model.password, model.remeberMe,false);
                
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.email);
                    if (_userManager.IsInRoleAsync(user,"Admin").Result)
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }   
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDAO model)
        {
            model.returnUrl ??= Url.Content("~/");
            model.externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                if (_userManager.FindByEmailAsync(model.email) != null)
                {
                    AppUser user = new AppUser
                    {
                        UserName = model.email,
                        Email = model.email,
                        PhoneNumber = model.phonenumber
                    };
                    var result = _userManager.CreateAsync(user, model.password).Result;
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "User");

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        if (_userManager.IsInRoleAsync(user, "Admin").Result)
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }

                }
            }
            return View();
        }
        public async Task<IActionResult> Logout(string returnUrl)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
