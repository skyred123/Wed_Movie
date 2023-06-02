using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using System.Data;
using Wed_Movie.DAO;
using Wed_Movie.Data.BLL;
using Wed_Movie.DI;
using Wed_Movie.Functions;
using Wed_Movie.Helpers;
using Wed_Movie.Models;

namespace Wed_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PhimController : Controller
    {
        private readonly IUploadFile _upLoadFile;

        public PhimController(IUploadFile upLoadFile)
        {
            _upLoadFile = upLoadFile;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public JsonResult GetPhim(string id)
        {
            try
            {
                return Json(new { code = 200, phim = PhimBLL.Item(id) });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy Thể Loại Thất Bại:" + ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetDSPhim(string search)
        {
            try
            {
                return Json(new { code = 200, dsDienVien = PhimBLL.List(search) });
            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy Phim:" + ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddPhimAsync(PhimDAO phimDAO)
        {
            if (ModelState.IsValid)
            {
                if (phimDAO != null)
                {
                    var phim = new Phim() { Id = Guid.NewGuid().ToString(), Name = phimDAO.Name };

                    if (PhimBLL.Add(phim))
                    {
                        return Json(new { code = 200, msg = "Thêm phim thành công:" });
                    }
                }
                return Json(new { code = 500, msg = "Thêm phim Thất Bại:" });
            }
            else
            {
                return Json(new { code = 500, msg = "Thêm phim Thất Bại:" });
            }
        }
        [HttpPost]
        public JsonResult UpdatePhim(PhimDAO phimDAO)
        {
            var phim = new Phim()
            {
                Id = phimDAO.Id,
                Name = phimDAO.Name,
            };
            if (PhimBLL.Update(phim))
            {
                return Json(new { code = 200, msg = "Lưu Thành công" });
            }
            return Json(new { code = 500, msg = "Lưu Thể Loại Thất Bại:" });
        }

        [HttpPost]
        public JsonResult DeletePhim(string id)
        {
            if (PhimBLL.Delete(id))
            {
                return Json(new { code = 200, msg = "Xóa Thành công" });
            }
            return Json(new { code = 500, msg = "Xóa Thể Loại Thất Bại:" });
        }

    }
}
