using Microsoft.AspNetCore.Mvc;
using Wed_Movie.DAO;
using Wed_Movie.Data.BLL;
using Wed_Movie.DI;
using Wed_Movie.Functions;
using Wed_Movie.Models;

namespace Wed_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TapPhimController : Controller
    {
        private readonly IUploadFile _upLoadFile;
        public TapPhimController(IUploadFile upLoadFile)
        {
            _upLoadFile = upLoadFile;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            ViewBag.IdPhanPhim = id;
            return PartialView("Index");
        }

        public JsonResult GetTapPhim(string id)
        {
            try
            {
                var check = TapPhimBLL.Item(id);
                return Json(new { code = 200, item = TapPhimBLL.Item(id) });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy dữ liệu Thất Bại:" });
            }
        }
        [HttpGet]
        public JsonResult GetListTapPhim(string id)
        {
            try
            {
                return Json(new { code = 200, listTapPhim = TapPhimBLL.ListTapPhim(id).OrderByDescending(e => e.Count).ToList() });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy dữ liệu Thất Bại:" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddTapPhim(TapPhimDao tapPhimDao)
        {
            if (ModelState.IsValid)
            {
                if (tapPhimDao == null)
                {
                    return Json(new { code = 500, msg = " Thất Bại:" });
                }
                TapPhim tapPhim = new TapPhim
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = tapPhimDao.Name,
                    IdPhanPhim = tapPhimDao.PhanPhim,
                    TimeUpdate = DateTime.Now.Date.ToString("yyyy-MM-dd"),
                    Count = int.Parse(tapPhimDao.Count),
                };
                var upload = await _upLoadFile.UploadsAsync(tapPhimDao.UrlVideo, true);
                if (upload.IsSuccess)
                {
                    tapPhim.UrlVideo = upload.filePath;
                    tapPhim.UrlImage = upload.ThumbFilePath;
                }
                if (TapPhimBLL.Add(tapPhim))
                {
                    return Json(new { code = 200, msg = "Thêm mới Thành công" });
                }
                return Json(new { code = 500, msg = "Thêm mới Thất Bại:" });
            }
            return Json(new { code = 500, msg = "Thêm mới Thất Bại:" });
        }

        [HttpDelete]
        public JsonResult DeleteTapPhim(string id)
        {
            var tapphim = TapPhimBLL.Item(id);
            if (tapphim.UrlImage == null || tapphim.UrlVideo == null)
            {
                if (tapphim.UrlImage == null && _upLoadFile.DeleteFile(tapphim.UrlVideo))
                {
                    if (TapPhimBLL.Delete(tapphim))
                    {
                        return Json(new { code = 200, msg = "Xóa Thành công" });
                    }
                }
                else if (tapphim.UrlVideo == null && _upLoadFile.DeleteFile(tapphim.UrlImage))
                {
                    if (TapPhimBLL.Delete(tapphim))
                    {
                        return Json(new { code = 200, msg = "Xóa Thành công" });
                    }
                }
                else
                {
                    if (TapPhimBLL.Delete(tapphim))
                    {
                        return Json(new { code = 200, msg = "Xóa Thành công" });
                    }
                }
            }
            if (tapphim != null &&  _upLoadFile.DeleteFile(tapphim.UrlImage) && _upLoadFile.DeleteFile(tapphim.UrlVideo))
            {
                if (TapPhimBLL.Delete(tapphim))
                {
                    return Json(new { code = 200, msg = "Xóa Thành công" });
                }
            }
            return Json(new { code = 500, msg = "Xóa Thể Loại Thất Bại:" });
        }
    }
}
