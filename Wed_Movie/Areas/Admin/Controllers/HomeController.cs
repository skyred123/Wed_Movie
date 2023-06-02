using Imgix;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
using Wed_Movie.Data;

namespace Wed_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            /*string baseUrl = "student-0732.imgix.net";
            string apiKey = "6463df7a89fadbf92eb4db93";
            string imagePath = "86e8bf37-91d9-4c26-bb0c-bda0788c1a83_Screenshot (4).png";


            var test = new UrlBuilder("student-0732.imgix.net", "6463df7a89fadbf92eb4db93",false);

            var imageUrl = test.BuildUrl("/86e8bf37-91d9-4c26-bb0c-bda0788c1a83_Screenshot (4).png");*//* https://domain.imgix.net/test/gaiman.jpg?s=51033c27726f19c0f8229a1ed2dc8523*//*
            ViewBag.ImageUrl = imageUrl;*/
            return View();
        }
    }
}
