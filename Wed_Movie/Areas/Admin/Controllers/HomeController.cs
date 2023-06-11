using Imgix;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieModel.Config;
using MovieModel.Service;

namespace Wed_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
