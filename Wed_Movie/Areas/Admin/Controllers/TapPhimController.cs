using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieModel.Config;
using MovieModel.Service;
using Wed_Movie.DAO;
using Wed_Movie.DI;
using Wed_Movie.Entities;

namespace Wed_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TapPhimController : Controller
    {
        private readonly IUploadFile _upLoadFile;
        private readonly TapPhimService _tapPhimService;
        private readonly TransactionService _transactionService;
        public TapPhimController(IUploadFile upLoadFile, TapPhimService tapPhimService, TransactionService transactionService)
        {
            _upLoadFile = upLoadFile;
            _tapPhimService = tapPhimService;
            _transactionService = transactionService;
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
                return Json(new { code = 200, item = _tapPhimService.GetAllTapPhimId(id).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy dữ liệu Thất Bại:" });
            }
        }
        [HttpGet]
        public JsonResult GetListTapPhim(string id, string? search)
        {
            try
            {
                var listtapphim = new List<TapPhim>();
                if (!id.IsNullOrEmpty())
                {
                    listtapphim = _tapPhimService.GetTapPhimByPhanPhimId(id).ToList();
                }
                else if(!search.IsNullOrEmpty())
                {
                    listtapphim = _tapPhimService.SearchNameTapPhims(search).ToList();
                }
                return Json(new { code = 200, listTapPhim = listtapphim });
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
                return Json(new { code = 500, msg = "Thêm mới Thất Bại:" });
            }
            try
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
                else
                {
                    return Json(new { code = 500, msg = "Thêm mới Thất Bại:" });
                }
                _transactionService.ExecuteTransaction(() => _tapPhimService.AddTapPhim(tapPhim));
                return Json(new { code = 200, msg = "Thêm mới Thành công" });
            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới Thất Bại:" });
            }
        }


        [HttpPut]
        public JsonResult UpdateTapPhim(TapPhimDao tapPhimDao)
        {
            return Json(new { code = 500, msg = "Xóa Thể Loại Thất Bại:" });
        }

        [HttpDelete]
        public JsonResult DeleteTapPhim(string id)
        {
            var tapphim = _tapPhimService.GetAllTapPhimId(id).FirstOrDefault();
            if (tapphim != null)
            {
                _upLoadFile.DeleteFile(tapphim.UrlVideo);
                _upLoadFile.DeleteFile(tapphim.UrlImage);
                _transactionService.ExecuteTransaction(() => _tapPhimService.DeleteTapPhim(id));
                return Json(new { code = 200, msg = "Xóa Thành công" });
            }
            return Json(new { code = 500, msg = "Xóa Thất Bại:" });
        }
    }
}
