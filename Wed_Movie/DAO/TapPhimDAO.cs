using System.ComponentModel.DataAnnotations;
using Wed_Movie.Models;

namespace Wed_Movie.DAO
{
    public class TapPhimDao
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tập phim!")]
        [Display(Name = "Tập Phim")]
        public string? Name { get; set; }

        public string Count { get; set; }
        public int? Rating { get; set; }
        public IFormFile? UrlVideo { get; set; }

        public string? PhanPhim { get; set; }
    }
}
