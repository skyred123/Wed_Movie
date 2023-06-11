using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wed_Movie.DAO;
using Wed_Movie.Entities;

namespace Wed_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            List<ApplicationUser> users = _userManager.Users.ToList();
            List<UserDAO> model = new List<UserDAO>();
            foreach (var item in users) {

                model.Add(new UserDAO { user = item });
            }
            return View();
        }
    }
}
