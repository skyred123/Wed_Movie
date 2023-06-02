using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wed_Movie.Models
{
    public class CT_Hang
    {
        [Key]
        [Column(Order = 1)]
        public string? IdHang { get; set; }
        [Key]
        [Column(Order = 2)]
        public string? IdPhanPhim { get; set; }

        [ForeignKey("IdHang")]
        public Hang? Hang { get; set; }
        [ForeignKey("IdPhanPhim")]
        public PhanPhim? PhanPhim { get; set; }
    }
}
