using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Configuration;
using Wed_Movie.DAO;
using Wed_Movie.Data;
using Wed_Movie.Data.BLL;
using Wed_Movie.DI;
using Wed_Movie.Helpers;
using Wed_Movie.Migrations;
using Wed_Movie.Models;

namespace Wed_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhanPhimController : Controller
    {
        private readonly IUploadFile _upLoadFile;
        public PhanPhimController(IUploadFile upLoadFile)
        {
            _upLoadFile = upLoadFile;
        }
        public IActionResult Index()
        {
            ViewBag.CT_DienVien = new SelectList(DienVienBLL.List(""), "Id", "Name");
            ViewBag.CT_Hangs =  new SelectList(HangBLL.List(""), "Id", "Name");
            ViewBag.CT_TheLoais = new SelectList(TheLoaiBLL.List(""), "Id", "Name");
            ViewBag.Phim = new SelectList(PhimBLL.List(""),"Id","Name");
            return View();
        }
        public JsonResult GetPhanPhim(string id)
        {
            try
            {
                return Json(new { code = 200, item = PhanPhimBLL.Item(id) });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy dữ liệu Thất Bại:" });
            }
        }
        [HttpGet]
        public JsonResult ListPhanPhim()
        {
            try
            {
                return Json(new { code = 200, listPhanPhim = PhanPhimBLL.List("").OrderByDescending(e=> DateTime.Parse(e.TimeUpdate)).ToList() });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy dữ liệu Thất Bại:" });
            }
        }


        [HttpPost]
        public async Task<JsonResult> AddPhanPhim(PhanPhimDAO phanPhimDAO)
        {
            if (ModelState.IsValid)
            {
                if (phanPhimDAO == null)
                {
                    return Json(new { code = 500, msg = " Thất Bại:" });
                }
                PhanPhim phanPhim = new PhanPhim() {
                    Id = Guid.NewGuid().ToString(),
                    Name = phanPhimDAO.Name,
                    Description = phanPhimDAO.Description,
                    PublicYear = phanPhimDAO.PublicYear,
                    PhimId = phanPhimDAO.Phim,
                    TimeUpdate = DateTime.Now.Date.ToString("yyyy-MM-dd"),
                };
                if(phanPhimDAO.CT_DienVien != null)
                {
                    phanPhim.CT_DienVien = CT_DienVienBLL.ListCT_DienVien(phanPhimDAO.CT_DienVien, phanPhim.Id);
                }
                if (phanPhimDAO.CT_Hangs != null )
                {
                    phanPhim.CT_Hangs = CT_HangBLL.ListCT_Hang(phanPhimDAO.CT_Hangs, phanPhim.Id);
                }
                if (phanPhimDAO.CT_TheLoais != null  )
                {
                    phanPhim.CT_TheLoais = CT_TheLoaiBLL.ListCT_TheLoai(phanPhimDAO.CT_TheLoais, phanPhim.Id);
                }
                var upload = await _upLoadFile.UploadsAsync(phanPhimDAO.Image, false);
                if (upload.IsSuccess)
                {
                    phanPhim.Image = upload.filePath;
                }
                upload = await _upLoadFile.UploadsAsync(phanPhimDAO.Trailer, false);
                if (upload.IsSuccess)
                {
                    phanPhim.Trailer = upload.filePath;
                }
                if (PhanPhimBLL.Add(phanPhim))
                {
                    return Json(new { code = 200, msg = "Thêm mới Thành công" });
                }
                return Json(new { code = 500, msg = "Thêm mới Thất Bại:" });
            }
            return Json(new { code = 500, msg = "Thêm mới Thất Bại:" });
        }
        [HttpPut]
        public async Task<JsonResult> UpdatePhanPhim(PhanPhimDAO phanPhimDAO)
        {
            if (ModelState.IsValid)
            {
                if (phanPhimDAO == null)
                {
                    return Json(new { code = 500, msg = "Thất Bại:" });
                }

                var temp = PhanPhimBLL.Item(phanPhimDAO.Id);

                PhanPhim phanPhim = new PhanPhim()
                {
                    Id = phanPhimDAO.Id,
                    Name = phanPhimDAO.Name,
                    Description = phanPhimDAO.Description,
                    PublicYear = phanPhimDAO.PublicYear,
                    PhimId = phanPhimDAO.Phim,
                    TimeUpdate = DateTime.Now.Date.ToString("yyyy-MM-dd"),
                };

                phanPhim.CT_DienVien = phanPhimDAO.CT_DienVien != null ?
                    CT_DienVienBLL.ListCT_DienVien(phanPhimDAO.CT_DienVien, phanPhim.Id) :
                    temp.CT_DienVien;

                phanPhim.CT_Hangs = phanPhimDAO.CT_Hangs != null ?
                    CT_HangBLL.ListCT_Hang(phanPhimDAO.CT_Hangs, phanPhim.Id) :
                    temp.CT_Hangs;

                phanPhim.CT_TheLoais = phanPhimDAO.CT_TheLoais != null ?
                    CT_TheLoaiBLL.ListCT_TheLoai(phanPhimDAO.CT_TheLoais, phanPhim.Id) :
                    temp.CT_TheLoais;

                if (phanPhimDAO.Image != null && _upLoadFile.DeleteFile(temp.Image))
                {
                    var upload = await _upLoadFile.UploadsAsync(phanPhimDAO.Image, false);
                    if (upload.IsSuccess)
                    {
                        phanPhim.Image = upload.filePath;
                    }
                }
                else
                {
                    phanPhim.Image = temp.Image;
                }

                if (phanPhimDAO.Trailer != null && _upLoadFile.DeleteFile(temp.Trailer))
                {
                    var upload = await _upLoadFile.UploadsAsync(phanPhimDAO.Trailer, false);
                    if (upload.IsSuccess)
                    {
                        phanPhim.Trailer = upload.filePath;
                    }
                }
                else
                {
                    phanPhim.Trailer = temp.Trailer;
                }

                if (PhanPhimBLL.Update(phanPhim))
                {
                    return Json(new { code = 200, msg = "Thêm mới Thành công" });
                }

                return Json(new { code = 500, msg = "Thêm mới Thất Bại:" });
            }

            return Json(new { code = 500, msg = "Thêm mới Thất Bại:" });
        }
        [HttpDelete]
        public JsonResult DeletePhanPhim(string id)
        {
            var dienvien = PhanPhimBLL.Item(id);
            if (dienvien != null && _upLoadFile.DeleteFile(dienvien.Image) && _upLoadFile.DeleteFile(dienvien.Trailer))
            {
                if ( PhanPhimBLL.Delete(dienvien))
                {
                    return Json(new { code = 200, msg = "Xóa Thành công" });
                }
            }
            
            return Json(new { code = 500, msg = "Xóa Thể Loại Thất Bại:" });
        }
    }
}
