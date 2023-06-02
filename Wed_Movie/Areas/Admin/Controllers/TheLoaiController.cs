using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wed_Movie.DAO;
using Wed_Movie.Data;
using Wed_Movie.Models;
using Wed_Movie.Functions;
using Wed_Movie.Data.BLL;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using React.AspNet;
using React;
using Wed_Movie.Helpers;

namespace Wed_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TheLoaiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        
        public JsonResult GetTheLoai(string id)
        {
            try
            {
                return Json(new { code = 200, theLoai = TheLoaiBLL.Item(id)});
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
                return Json(new { code = 200, dsTheLoai = TheLoaiBLL.List(search) });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg= "Lấy Thể Loại Thất Bại:"+ex.Message });
            }
        }
        [HttpPost]
        public IActionResult AddTheLoai(TheLoaiDAO model)
        {
            if (ModelState.IsValid)
            {
                var theloai = new TheLoai()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name,
                };
                if (TheLoaiBLL.Add(theloai))
                {
                    return Json(new { code = 200, msg = "Lưu Thành công" });
                }
            }
            return Json(new { code = 500, msg = "Lưu Thể Loại Thất Bại:" });
        }
        [HttpPost]
        public JsonResult UpdateTheLoai(string id,string name)
        {
            var theloai = new TheLoai()
            {
                Id = id,
                Name = name,
            };
            if (TheLoaiBLL.Update(theloai))
            {
                return Json(new { code = 200, msg = "Lưu Thành công" });
            }
            return Json(new { code = 500, msg = "Lưu Thể Loại Thất Bại:" });
        }
        [HttpPost]
        public JsonResult DeleteTheLoai(string id)
        {
            if (TheLoaiBLL.Delete(id))
            {
                return Json(new { code = 200, msg = "Xóa Thành công" });
            }
            return Json(new { code = 500, msg = "Xóa Thể Loại Thất Bại:" });
        }
    }
}
