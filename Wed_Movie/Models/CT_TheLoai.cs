using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wed_Movie.Models
{
    public class CT_TheLoai
    {
        [Key]
        [Column(Order = 1)]
        public string? IdTheLoai { get; set; }
        [Key]
        [Column(Order = 2)]
        public string? IdPhanPhim { get; set; }

        [ForeignKey("IdTheLoai")]
        public TheLoai? TheLoai { get; set; }
        [ForeignKey("IdPhanPhim")]
        public PhanPhim? PhanPhim { get; set; }
    }
}
