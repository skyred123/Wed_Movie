using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Wed_Movie.Models
{
    public class Phim
    {
        [StringLength(128)]
        public string? Id { get; set; }

        public string? Name { get; set; }
        public List<PhanPhim>? PhanPhim { get; set; }
    } 
}
