using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Wed_Movie.Data.BLL;
using Wed_Movie.Helpers;
using Wed_Movie.Models;

namespace Wed_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HangController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public JsonResult GetHang(string id)
        {
            try
            {
                return Json(new { code = 200, hang = HangBLL.Item(id) });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy Thể Loại Thất Bại:" + ex.Message });
            }
        }
        [HttpPost]
        public JsonResult GetDsHang(string search)
        {
            try
            {
                return Json(new { code = 200, dsHang = HangBLL.List(search) });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy Thể Loại Thất Bại:" + ex.Message });
            }
        }
        [HttpPost]
        public JsonResult AddHang(string name)
        {
            if (ModelState.IsValid)
            {
                var hang = new Hang()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                };
                if (HangBLL.Add(hang))
                {
                    return Json(new { code = 200, msg = "Thêm mới Thành công" });
                }
            }
            return Json(new { code = 500, msg = "Thêm Thể Loại Thất Bại:" });
        }
        [HttpPost]
        public JsonResult UpdateHang(string id, string name)
        {
            var hang = new Hang()
            {
                Id = id,
                Name = name,
            };
            if (HangBLL.Update(hang))
            {
                return Json(new { code = 200, msg = "Lưu Thành công" });
            }
            return Json(new { code = 500, msg = "Lưu Thể Loại Thất Bại:" });
        }
        [HttpPost]
        public JsonResult DeleteHang(string id)
        {
            if (HangBLL.Delete(id))
            {
                return Json(new { code = 200, msg = "Xóa Thành công" });
            }
            return Json(new { code = 500, msg = "Xóa Thể Loại Thất Bại:" });
        }
    }
}
