using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class PhimController : Controller
    {
        private readonly IUploadFile _upLoadFile;
        private readonly PhimService _phimService;
        private readonly TransactionService _transactionService;

        public PhimController(IUploadFile upLoadFile,PhimService phimService, TransactionService transactionService)
        {
            _upLoadFile = upLoadFile;
            _phimService = phimService;
            _transactionService = transactionService;
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
                return Json(new { code = 200, phim = _phimService.GetPhimId(id).FirstOrDefault()});
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
                var listPhim = new List<Phim>();
                if (search.IsNullOrEmpty())
                {
                    listPhim = _phimService.GetListPhims().ToList();
                }
                else
                {
                    listPhim = _phimService.SearchNamePhims(search).ToList();
                }
                return Json(new { code = 200, dsDienVien = listPhim });
            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy Phim:" + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult AddPhim(PhimDAO phimDAO)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 500, msg = "Thêm phim Thất Bại:" });
            }
            try
            {
                var phim = new Phim()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = phimDAO.Name
                };

                _transactionService.ExecuteTransaction(() => _phimService.AddPhim(phim));
                return Json(new { code = 200, msg = "Thêm phim thành công:" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm phim Thất Bại: " + ex.Message });
            }
        }
        [HttpPost]
        public JsonResult UpdatePhim(PhimDAO phimDAO)
        {
            try
            {
                var phim = new Phim()
                {
                    Id = phimDAO.Id,
                    Name = phimDAO.Name,
                };
                _transactionService.ExecuteTransaction(() => _phimService.UpdatePhim(phim));
                return Json(new { code = 200, msg = "Lưu phim Thành công" });
            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Lưu phim Thất Bại:" });
            }
        }

        [HttpPost]
        public JsonResult DeletePhim(string id)
        {
            try
            {
                _transactionService.ExecuteTransaction(() => _phimService.DeletePhim(id));
                return Json(new { code = 200, msg = "Xóa phim Thành công" });

            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa phim Thất Bại:" });
            }
        }

    }
}
