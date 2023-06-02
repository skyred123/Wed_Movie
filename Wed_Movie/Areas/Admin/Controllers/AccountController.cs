using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wed_Movie.DAO;
using Wed_Movie.Data;

namespace Wed_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            List<AppUser> users = _userManager.Users.ToList();
            List<UserDAO> model = new List<UserDAO>();
            foreach (var item in users) {

                model.Add(new UserDAO { user = item });
            }
            return View();
        }
    }
}
