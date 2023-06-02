using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Wed_Movie.Models;

namespace Wed_Movie.DAO
{
    public class CT_TheLoaiDAO
    {
        public string? IdTheLoai { get; set; }
        public string? IdPhim { get; set; }

        public bool isCheck { get; set; }
        public TheLoai? TheLoai { get; set; }

        public Phim? Phim { get; set; }
    }
}
