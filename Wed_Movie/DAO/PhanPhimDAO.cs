using System.ComponentModel.DataAnnotations;
using Wed_Movie.Models;

namespace Wed_Movie.DAO
{
    public class PhanPhimDAO
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên phim!")]
        [Display(Name = "Tên Phim")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập nội dung!")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn năm!")]
        [Display(Name = "Năm")]
        public string? PublicYear { get; set; }

        
        public IFormFile? Image { get; set; }
        public IFormFile? Trailer { get; set; }
        public string? Phim { get; set; }


        public List<string>? CT_DienVien { get; set; }
        public List<TapPhim>? TapPhim { get; set; }

        public List<string>? CT_Hangs { get; set; }

        public List<string>? CT_TheLoais { get; set; }
    }
}
