using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.IdentityModel.Tokens;
using System.Buffers.Text;
using System.Data;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;
using Wed_Movie.DAO;
using Wed_Movie.Data.BLL;
using Wed_Movie.DI;
using Wed_Movie.Helpers;
using Wed_Movie.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Wed_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DienVienController : Controller
    {
        private readonly IUploadFile _upLoadFile;
        public DienVienController(IUploadFile upLoadFile)
        {
            _upLoadFile = upLoadFile;
        }

        public IActionResult Index()
        {
            var countries = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            .Select(c => new RegionInfo(c.Name).DisplayName)
            .Distinct()
            .ToList()
            .Order();

            ViewBag.Nationalities = countries;
            return View();
        }


        [HttpGet]

        public JsonResult GetDienVien(string id)
        {
            try
            {
                var dienvien = DienVienBLL.Item(id);
                string file ="";
                if (dienvien != null || dienvien.Image.IsNullOrEmpty())
                {
                    file = "data:image/jpeg;base64," + Convert.ToBase64String(_upLoadFile.GetFile(dienvien.Image));
                }   
                return Json(new { code = 200, dienVien = dienvien, img = (file == null) ? "" : file });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy Thể Loại Thất Bại:" + ex.Message });
            }
        }
        [HttpGet]
        public JsonResult GetDsDienVien(string search)
        {
            try
            {
                return Json(new { code = 200, dsDienVien = DienVienBLL.List(search).ToList() });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy Thể Loại Thất Bại:" + ex.Message });
            }
        }
        [HttpPost]
        public async Task<JsonResult> AddDienVienAsync(DienVienDAO dienVienDAO)
        {
            if (ModelState.IsValid)
            {
                var dienvien = new DienVien()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = dienVienDAO.Name,
                    Birthday = dienVienDAO.Birthday,
                    Nationality = dienVienDAO.Nationality,
                    Sex = dienVienDAO.Sex,
                    
                };
                if (dienVienDAO.Image == null)
                {
                    dienvien.Image = (dienVienDAO.Sex.Equals("Nam")) ? LinkImage.Avatar_Nam :
                                    (dienVienDAO.Sex.Equals("Nu")) ? LinkImage.Avatar_Nu :
                                    LinkImage.Avatar_Khac;
                }
                else
                {
                    var upload = await _upLoadFile.UploadsAsync(dienVienDAO.Image, false);
                    if (upload.IsSuccess)
                    {
                        dienvien.Image = upload.filePath;
                    }
                }
                if (DienVienBLL.Add(dienvien))
                {
                    return Json(new { code = 200, msg = "Thêm mới Thành công" });
                }
            }
            return Json(new { code = 500, msg = "Thêm Thể Loại Thất Bại:" });
        }
        [HttpPost]
        public async Task<JsonResult> UpdateDienVienAsync(DienVienDAO dienVienDAO)
        {
            var dienvien = new DienVien()
            {
                Id = dienVienDAO.Id,
                Name = dienVienDAO.Name,
                Birthday = dienVienDAO.Birthday,
                Nationality = dienVienDAO.Nationality,
                Sex = dienVienDAO.Sex,

            };
            var temp = DienVienBLL.Item(dienVienDAO.Id);
            if (dienVienDAO.Image != null && !dienVienDAO.Image.Equals(temp.Image))
            {
                var upload = await _upLoadFile.UploadsAsync(dienVienDAO.Image, false);
                if (upload.IsSuccess)
                {
                    dienvien.Image = upload.filePath;
                }
            }
            else
            {
                dienvien.Image = temp.Image;
            }
            if (DienVienBLL.Update(dienvien))
            {
                return Json(new { code = 200, msg = "Lưu Thành công" });
            }
            return Json(new { code = 500, msg = "Lưu Thể Loại Thất Bại:" });
        }
        [HttpPost]
        public JsonResult DeleteDienVien(string id)
        {
            var dienvien =  DienVienBLL.Item(id);
            if (dienvien.Image.Equals(LinkImage.Avatar_Nam) ==false && dienvien.Image.Equals(LinkImage.Avatar_Nu) ==false && dienvien.Image.Equals(LinkImage.Avatar_Khac) == false)
            {
                if (_upLoadFile.DeleteFile(dienvien.Image))
                {
                    if (DienVienBLL.Delete(dienvien))
                    {
                        return Json(new { code = 200, msg = "Xóa Thành công" });
                    }
                }
            }
            else
            {
                if (DienVienBLL.Delete(dienvien))
                {
                    return Json(new { code = 200, msg = "Xóa Thành công" });
                }
            }
            return Json(new { code = 500, msg = "Xóa Thể Loại Thất Bại:" });
        }
    }
}
