using Microsoft.AspNetCore.Mvc;
using Wed_Movie.DAO;
using Wed_Movie.Entities;
using Microsoft.AspNetCore.Authorization;
using MovieModel.Config;
using MovieModel.Service;
using Microsoft.IdentityModel.Tokens;

namespace Wed_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TheLoaiController : Controller
    {
        private readonly TheLoaiService _theloaiService;
        private readonly TransactionService _transactionService;
        public TheLoaiController(TheLoaiService theLoaiService, TransactionService transactionService)
        {
            _theloaiService = theLoaiService;
            _transactionService = transactionService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        
        public JsonResult GetTheLoai(string id)
        {
            try
            {
                return Json(new { code = 200, theLoai = _theloaiService.GetTheLoaiId(id).FirstOrDefault()});
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy Thể Loại Thất Bại:" + ex.Message });
            }
        }
        [HttpGet]
        public JsonResult GetDsTheLoai(string search)
        {
            try
            {
                var listTheLoai = new List<TheLoai>();
                if (search.IsNullOrEmpty())
                {
                    listTheLoai = _theloaiService.GetListTheLoais().ToList();
                }
                else
                {
                    listTheLoai = _theloaiService.SearchNameTheLoais(search).ToList();
                }
                return Json(new { code = 200, dsTheLoai = listTheLoai });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg= "Lấy Thể Loại Thất Bại: " + ex.Message });
            }
        }
        [HttpPost]
        public IActionResult AddTheLoai(TheLoaiDAO model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 500, msg = "Lưu Thể Loại Thất Bại:" });
            }
            try
            {
                var theloai = new TheLoai()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name,
                };
                _transactionService.ExecuteTransaction(() => _theloaiService.AddTheLoai(theloai));
                return Json(new { code = 200, msg = "Lưu Thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy Thể Loại Thất Bại: " + ex.Message });
            }
            
        }
        [HttpPost]
        public JsonResult UpdateTheLoai(string id,string name)
        {
            try
            {
                var theloai = new TheLoai()
                {
                    Id = id,
                    Name = name,
                };
                _transactionService.ExecuteTransaction(() => _theloaiService.UpdateTheLoai(theloai));
                return Json(new { code = 200, msg = "Lưu Thành công" });
            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Lưu Thể Loại Thất Bại:" });
            }
            
            
        }
        [HttpPost]
        public JsonResult DeleteTheLoai(string id)
        {
            try
            {
                _transactionService.ExecuteTransaction(() => _theloaiService.DeleteTheLoai(id));
                return Json(new { code = 200, msg = "Lưu Thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa Thể Loại Thất Bại:" });
            }
            
        }
    }
}
