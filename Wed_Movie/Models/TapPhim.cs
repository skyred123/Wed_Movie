using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wed_Movie.Models
{
    public class TapPhim
    {
        [StringLength(128)]
        public string? Id { get; set; } 

        public string? Name { get; set; }

        public int? Rating { get; set; }

        public string? UrlVideo { get; set; }

        public string? UrlImage { get; set; }

        public string? IdPhanPhim { get; set; }

        public string? TimeUpdate { get; set; }

        public int? Count { get; set; }
        [ForeignKey("IdPhanPhim")]
        public PhanPhim? PhanPhim { get; set; }
    }
}
