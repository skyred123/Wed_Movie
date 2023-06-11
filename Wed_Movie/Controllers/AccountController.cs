using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.FileProviders;
using MovieModel.Migrations;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Wed_Movie.DAO;
using Wed_Movie.Entities;
using Wed_Movie.Helpers;
using Wed_Movie.SendMail;

namespace Wed_Movie.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore, IEmailSender emailSender, IWebHostEnvironment webHostEnvironment)
        {
            _userStore = userStore;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
            _emailStore = GetEmailStore();
            _webHostEnvironment = webHostEnvironment;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return PartialView("Login");
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return PartialView("Register");
        }
        [AllowAnonymous]
        public IActionResult ProcessEmailConfirm()
        {
            return PartialView("ProcessEmailConfirm");
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
                    {
                        return Json(new { code = 200, redirect = Url.Action("Index", "Home", new { area = "Admin" }) });
                    }    
                    else
                    {
                        return Json(new { code = 200, redirect = Url.Action("Index", "Home") });
                    }
                }
                else
                {
                    return Json(new { code = 500, msg = "Đăng nhập không thành công" });
                }
            }
            return Json(new { code = 500, msg= "Đăng nhập không thành công" });
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDAO model)
        {
            model.returnUrl ??= Url.Content("~/");
            model.externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ApplicationUser user = new ApplicationUser
            {
                UserName = model.email,
                Email = model.email,
                PhoneNumber = model.phonenumber,
                TimeToken = DateTime.Now.ToString(),
            };
            if (!ModelState.IsValid)
            {
                return Json(new { code = 500, msg = "Đăng ký không thành công" });
            }
            var userEmail = await _userManager.FindByEmailAsync(model.email);

            if (userEmail != null && userEmail.EmailConfirmed)
            {
                return Json(new { code = 500, msg = "Tài khoản đã được." });
            }
            else if (userEmail != null && !userEmail.EmailConfirmed)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userId, code = code }, protocol: HttpContext.Request.Scheme);
                string viewPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Views\\Email\\EmailTemplate.cshtml");

                var html = RenderView.RenderViewToStringAsync(viewPath, HtmlEncoder.Default.Encode(callbackUrl));

                await _emailSender.SendEmailAsync(model.email, "Xác thực email", html);
                return Json(new { code = 200, msg = "Vui lòng kiểm tra email" });
            }
            else
            {
                await _userStore.SetUserNameAsync(user, model.email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, model.email, CancellationToken.None);
                var result = _userManager.CreateAsync(user, model.password).Result;
                if (result.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    string viewPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Views\\Email\\EmailTemplate.cshtml");

                    var html = RenderView.RenderViewToStringAsync(viewPath, HtmlEncoder.Default.Encode(callbackUrl));

                    await _emailSender.SendEmailAsync(model.email, "Xác thực email", html);
                }
                return Json(new { code = 200, msg = "Vui lòng kiểm tra email." });
            }
        }
        public async Task<IActionResult> Logout(string returnUrl)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            DateTime tokenDateTime = DateTime.Parse(user.TimeToken);
            TimeSpan timeElapsed = DateTime.Now - tokenDateTime;
            if (timeElapsed.TotalMinutes > 10)
            {
                return View("~/Views/Email/EmailExpired.cshtml");
            }
            else
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                var result = await _userManager.ConfirmEmailAsync(user, code);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");

                    var info = await _signInManager.GetExternalLoginInfoAsync();
                    if (info == null)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false,info.LoginProvider);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
        
        [AllowAnonymous]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                // Xử lý lỗi đăng nhập
                return View("Error");
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return View("Error");
            }
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (result.IsLockedOut)
            {
                return View("Error");
            }
            else
            {
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                    var newUser = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        TimeToken = DateTime.Now.ToString(),
                    };
                    var createResult = await _userManager.CreateAsync(newUser);
                    if (createResult.Succeeded)
                    {
                        // Thêm thông tin xác thực bên ngoài vào tài khoản mới
                        var addLoginResult = await _userManager.AddLoginAsync(newUser, info);
                        if (addLoginResult.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(newUser, "User");
                            // Đăng nhập người dùng vào tài khoản mới
                            await _signInManager.SignInAsync(newUser, isPersistent: false);
                            return RedirectToAction("Index", "Home");
                        }
                        return View("Error");
                    }
                }
                ViewData["ShowModal"] = "ProcessEmail";
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> ProcessEmail(ProcessEmailConfirmDAO processEmailConfirmDAO)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return Json(new { code = 300, redirect = Url.Action("Error", "Home") });
            }
            if (!ModelState.IsValid)
            {
                return Json(new { code = 500, msg = "Đăng ký không thành công" });
            }
            var user = new ApplicationUser
            {
                UserName = processEmailConfirmDAO.email,
                Email = processEmailConfirmDAO.email,
                TimeToken = DateTime.Now.ToString(),
            };
            var createResult = await _userManager.CreateAsync(user);
            if (createResult.Succeeded)
            {
                var result = await _userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    string viewPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Views\\Email\\EmailTemplate.cshtml");

                    var html = RenderView.RenderViewToStringAsync(viewPath, HtmlEncoder.Default.Encode(callbackUrl));

                    await _emailSender.SendEmailAsync(user.Email, "Xác thực email", html);
                }
            }

            return Json(new { code = 200, msg = "Vui lòng kiểm tra email." });
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }


    }
}
