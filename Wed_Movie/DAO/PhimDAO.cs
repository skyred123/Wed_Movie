using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Wed_Movie.Models;

namespace Wed_Movie.DAO
{
    public class PhimDAO
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên phim!")]
        [Display(Name = "Tên Phim")]
        public string? Name { get; set; }

        public List<PhanPhim>? PhanPhims { get; set; }

    }
}
