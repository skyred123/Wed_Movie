using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wed_Movie.Entities
{
    public class PhanPhim
    {
        [StringLength(128)]
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? PublicYear { get; set; }

        public string? TimeUpdate { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public string? Trailer { get; set; }

        [StringLength(128)]
        public string? PhimId { get; set; }
        [ForeignKey("PhimId")]
        public Phim? Phim { get; set; }

        public List<CT_DienVien?>? CT_DienVien { get; set; }
        public List<TapPhim?>? TapPhim { get; set; }

        public List<CT_Hang?>? CT_Hangs { get; set; }

        public List<CT_TheLoai?>? CT_TheLoais { get; set; }
    }
}
