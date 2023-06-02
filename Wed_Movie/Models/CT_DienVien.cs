using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wed_Movie.Models
{
    public class CT_DienVien
    {
        [Key]
        [Column(Order = 1)]
        public string? IdDienVien { get; set; }
        [Key]
        [Column(Order = 2)]
        public string? IdPhanPhim { get; set; }

        [ForeignKey("IdDienVien")]
        public DienVien? DienVien { get; set; }
        [ForeignKey("IdPhanPhim")]
        public PhanPhim? PhanPhim { get; set; }
    }
}
