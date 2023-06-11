using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieModel.Config;
using MovieModel.Service;
using Wed_Movie.Entities;

namespace Wed_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HangController : Controller
    {
        private readonly HangService _hangService;
        private readonly TransactionService _transactionService;

        public HangController(HangService hangService, TransactionService transactionService)
        {
            _hangService = hangService;
            _transactionService = transactionService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public JsonResult GetHang(string id)
        {
            try
            {
                return Json(new { code = 200, hang = _hangService.GetHangId(id).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy Thất Bại: " + ex.Message });
            }
        }
        [HttpPost]
        public JsonResult GetDsHang(string search)
        {
            try
            {
                var listHang = new List<Hang>();
                if (search.IsNullOrEmpty())
                {
                    listHang = _hangService.GetListHangs().ToList();
                }
                else
                {
                    listHang = _hangService.SearchNameHangs(search).ToList();
                }
                return Json(new { code = 200, dsHang = listHang });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy Thất Bại:" + ex.Message });
            }
        }
        [HttpPost]
        public JsonResult AddHang(string name)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 500, msg = "Thêm mới Thất Bại:" });
            }

            try
            {
                var hang = new Hang()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                };
                _transactionService.ExecuteTransaction(() => _hangService.AddHang(hang));
                return Json(new { code = 200, msg = "Thêm mới Thành công" });
            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới Thất Bại:" });
            }
            
        }
        [HttpPost]
        public JsonResult UpdateHang(string id, string name)
        {
            try
            {
                var hang = new Hang()
                {
                    Id = id,
                    Name = name,
                };
                _transactionService.ExecuteTransaction(() => _hangService.UpdateHang(hang));
                return Json(new { code = 200, msg = "Cập nhật Thành công" });

            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Lưu Thất Bại:" });
            }
        }
        [HttpPost]
        public JsonResult DeleteHang(string id)
        {
            try 
            {
                _transactionService.ExecuteTransaction(() => _hangService.DeleteHang(id));
                return Json(new { code = 200, msg = "Xóa Thành công" });

            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa Thất Bại:" });
            }
        }
    }
}
