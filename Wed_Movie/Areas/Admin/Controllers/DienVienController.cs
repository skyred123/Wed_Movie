using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.IdentityModel.Tokens;
using Wed_Movie.DAO;
using Wed_Movie.DI;
using Wed_Movie.Helpers;
using Wed_Movie.Entities;
using MovieModel.Service;
using MovieModel.Config;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Wed_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DienVienController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUploadFile _upLoadFile;
        private readonly DienVienService _dienVienService;
        private readonly TransactionService _transactionService;
        public DienVienController(IWebHostEnvironment webHostEnvironment, IUploadFile upLoadFile, DienVienService dienVienService, TransactionService transactionService)
        {
            _hostingEnvironment = webHostEnvironment;
            _upLoadFile = upLoadFile;
            _dienVienService = dienVienService;
            _transactionService = transactionService;
        }

        public IActionResult Index()
        {
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Json/countries.json");

            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string json = streamReader.ReadToEnd();
                List<Countries> items = JsonConvert.DeserializeObject<List<Countries>>(json);
                ViewBag.Nationalities = new SelectList(items, "code", "name");
            }
            return View();
        }


        [HttpGet]

        public JsonResult GetDienVien(string id)
        {
            try
            {
                var dienvien = _dienVienService.GetAllDienVienId(id).FirstOrDefault();
                string file ="";
                if (dienvien != null || dienvien.Image.IsNullOrEmpty())
                {
                    file = "data:image/jpeg;base64," + Convert.ToBase64String(_upLoadFile.GetFile(dienvien.Image));
                }   
                return Json(new { code = 200, dienVien = dienvien, img = (file == null) ? "" : file });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy Thất Bại:" + ex.Message });
            }
        }
        [HttpGet]
        public JsonResult GetDsDienVien(string search)
        {
            try
            {
                var listDienVien = new List<DienVien>();
                if (search.IsNullOrEmpty())
                {
                    listDienVien = _dienVienService.GetListDienViens().ToList();
                }
                else
                {
                    listDienVien = _dienVienService.SearchNameDienViens(search).ToList();
                }
                return Json(new { code = 200, dsDienVien = listDienVien });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy mới Thất Bại:" + ex.Message });
            }
        }
        [HttpPost]
        public async Task<JsonResult> AddDienVienAsync(DienVienDAO dienVienDAO)
        {
            if (!ModelState.IsValid) return Json(new { code = 500, msg = "Thêm mới Thất Bại:" });

            try
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
                _transactionService.ExecuteTransaction(() => _dienVienService.AddDienVien(dienvien));
                return Json(new { code = 200, msg = "Thêm mới Thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới Thất Bại:" });
            }
        }
        [HttpPost]
        public async Task<JsonResult> UpdateDienVienAsync(DienVienDAO dienVienDAO)
        {
            try
            {
                var dienvien = new DienVien()
                {
                    Id = dienVienDAO.Id,
                    Name = dienVienDAO.Name,
                    Birthday = dienVienDAO.Birthday,
                    Nationality = dienVienDAO.Nationality,
                    Sex = dienVienDAO.Sex,

                };
                var temp = _dienVienService.GetAllDienVienId(dienvien.Id).FirstOrDefault();
                if (dienVienDAO.Image != null && !dienVienDAO.Image.Equals(temp.Image))
                {
                    var upload = await _upLoadFile.UploadsAsync(dienVienDAO.Image, false);
                    if (upload.IsSuccess)
                    {
                        dienvien.Image = upload.filePath;
                    }
                    else
                    {
                        return Json(new { code = 500, msg = "Lưu Thể Loại Thất Bại:" });
                    }
                }
                else
                {
                    dienvien.Image = temp.Image;
                }
                _transactionService.ExecuteTransaction(() => _dienVienService.UpdateDienVien(dienvien));
                return Json(new { code = 200, msg = "Lưu Thành công" });
            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Lưu Thể Loại Thất Bại:" });
            }
        }
        [HttpPost]
        public JsonResult DeleteDienVien(string id)
        {
            try
            {
                var dienvien = _dienVienService.GetAllDienVienId(id).FirstOrDefault();
                if (dienvien.Image.Equals(LinkImage.Avatar_Nam) == false && dienvien.Image.Equals(LinkImage.Avatar_Nu) == false && dienvien.Image.Equals(LinkImage.Avatar_Khac) == false)
                {
                    if (!_upLoadFile.DeleteFile(dienvien.Image))
                    {
                        return Json(new { code = 500, msg = "Xóa Thể Loại Thất Bại:" });
                    }
                }
                _transactionService.ExecuteTransaction(() => _dienVienService.DeleteDienVien(id));
                return Json(new { code = 200, msg = "Xóa Thành công" });

            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa Thể Loại Thất Bại:" });
            }
        }
    }
}
