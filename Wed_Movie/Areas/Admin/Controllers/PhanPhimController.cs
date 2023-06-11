using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieModel.Config;
using MovieModel.Service;
using Wed_Movie.DAO;
using Wed_Movie.DI;
using Wed_Movie.Entities;

namespace Wed_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhanPhimController : Controller
    {
        private readonly IUploadFile _upLoadFile;

        private readonly PhanPhimService _phanPhimService;
        private readonly TransactionService _transactionService;
        public PhanPhimController(IUploadFile upLoadFile, PhanPhimService phanPhimService, TransactionService transactionService)
        {
            _upLoadFile = upLoadFile;
            _phanPhimService = phanPhimService;
            _transactionService = transactionService;
        }
        public IActionResult Index()
        {
            ViewBag.CT_DienVien = new SelectList(_phanPhimService.GetAllDienViens().ToList(), "Id", "Name");
            ViewBag.CT_Hangs =  new SelectList(_phanPhimService.GetListHang().ToList(), "Id", "Name");
            ViewBag.CT_TheLoais = new SelectList(_phanPhimService.GetListTheLoai().ToList(), "Id", "Name");
            ViewBag.Phim = new SelectList(_phanPhimService.GetListPhim().ToList(), "Id","Name");
            return View();
        }
        public JsonResult GetPhanPhim(string id)
        {
            try
            {
                return Json(new { code = 200, item = _phanPhimService.GetAllPhanPhimId(id).FirstOrDefault() });
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
                var check = _phanPhimService.GetListPhanPhims().ToList();
                return Json(new { code = 200, listPhanPhim = _phanPhimService.GetListPhanPhims().ToList() });
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
                PhanPhim phanPhim = new PhanPhim()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = phanPhimDAO.Name,
                    Description = phanPhimDAO.Description,
                    PublicYear = phanPhimDAO.PublicYear,
                    PhimId = phanPhimDAO.Phim,
                    TimeUpdate = DateTime.Now.Date.ToString("yyyy-MM-dd"),
                };
                try
                {
                    if (phanPhimDAO == null)
                    {
                        return Json(new { code = 500, msg = " Thất Bại:" });
                    }

                    if (phanPhimDAO.DienViens != null) phanPhim.CT_DienVien = _phanPhimService.AddDienVienPhanPhim(phanPhimDAO.DienViens,phanPhimDAO.Id).ToList();

                    if (phanPhimDAO.CT_Hangs != null) phanPhim.CT_Hangs = _phanPhimService.AddHangPhanPhim(phanPhimDAO.CT_Hangs,phanPhimDAO.Id).ToList();

                    if (phanPhimDAO.CT_TheLoais != null) phanPhim.CT_TheLoais = _phanPhimService.AddTheLoaiPhanPhim(phanPhimDAO.CT_TheLoais,phanPhimDAO.Id).ToList();

                    var upload = await _upLoadFile.UploadsAsync(phanPhimDAO.Image, false);
                    if (upload.IsSuccess) phanPhim.Image = upload.filePath;
                    else return Json(new { code = 500, msg = "Thêm mới Thất Bại:" });

                    upload = await _upLoadFile.UploadsAsync(phanPhimDAO.Trailer, false);
                    if (upload.IsSuccess) phanPhim.Trailer = upload.filePath;
                    else return Json(new { code = 500, msg = "Thêm mới Thất Bại:" });

                    _transactionService.ExecuteTransaction(() => _phanPhimService.AddPhanPhim(phanPhim));
                    return Json(new { code = 200, msg = "Thêm mới Thành công" });
                }
                catch (Exception ex)
                {
                    if (_upLoadFile.CheckFileExists(phanPhim.Image))
                    {
                        _upLoadFile.DeleteFile(phanPhim.Image);
                    }
                    if (_upLoadFile.CheckFileExists(phanPhim.Trailer))
                    {
                        _upLoadFile.DeleteFile(phanPhim.Trailer);
                    }
                    return Json(new { code = 500, msg = "Thêm mới Thất Bại:" });
                }
            }
            return Json(new { code = 500, msg = "Thêm mới Thất Bại:"  });
        }
        [HttpPut]
        public async Task<JsonResult> UpdatePhanPhim(PhanPhimDAO phanPhimDAO)
        {
            if (ModelState.IsValid)
            {
                return Json(new { code = 500, msg = "Cập nhật Thất Bại: "});
            }
            try
            {
                if (phanPhimDAO == null)
                {
                    return Json(new { code = 500, msg = "Thất Bại:" });
                }

                var temp = _phanPhimService.GetAllPhanPhimId(phanPhimDAO.Id).FirstOrDefault();

                PhanPhim phanPhim = new PhanPhim()
                {
                    Id = phanPhimDAO.Id,
                    Name = phanPhimDAO.Name,
                    Description = phanPhimDAO.Description,
                    PublicYear = phanPhimDAO.PublicYear,
                    PhimId = phanPhimDAO.Phim,
                    TimeUpdate = DateTime.Now.Date.ToString("yyyy-MM-dd"),
                };

                phanPhim.CT_DienVien = phanPhimDAO.DienViens != null ?
                    _phanPhimService.AddDienVienPhanPhim(phanPhimDAO.DienViens, phanPhimDAO.Id).ToList() :
                    temp.CT_DienVien;

                phanPhim.CT_Hangs = phanPhimDAO.CT_Hangs != null ?
                   _phanPhimService.AddHangPhanPhim(phanPhimDAO.CT_Hangs, phanPhimDAO.Id).ToList() :
                    temp.CT_Hangs;

                phanPhim.CT_TheLoais = phanPhimDAO.CT_TheLoais != null ?
                    phanPhim.CT_TheLoais = _phanPhimService.AddTheLoaiPhanPhim(phanPhimDAO.CT_TheLoais, phanPhimDAO.Id).ToList() : temp.CT_TheLoais;

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

                _transactionService.ExecuteTransaction(() => _phanPhimService.UpdatePhanPhim(phanPhim));
                return Json(new { code = 200, msg = "Thêm mới Thành công" });
            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật Thất Bại Thất Bại: " + ex.Message });
            }
        }
        [HttpDelete]
        public JsonResult DeletePhanPhim(string id)
        {
            var phanphim = _phanPhimService.GetAllPhanPhimId(id).FirstOrDefault();
            if (phanphim != null && _upLoadFile.CheckFileExists(phanphim.Image) && _upLoadFile.CheckFileExists(phanphim.Trailer))
            {
                _upLoadFile.DeleteFile(phanphim.Image);
                _upLoadFile.DeleteFile(phanphim.Trailer);
                _transactionService.ExecuteTransaction(() => _phanPhimService.DeletePhanPhim(id));
                return Json(new { code = 200, msg = "Xóa Thành công" });
            }

            return Json(new { code = 500, msg = "Xóa Thể Loại Thất Bại:" });
        }
    }
}
