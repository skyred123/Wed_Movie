using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Wed_Movie.Models
{
    public class TheLoai
    {
        [MaxLength(128)]
        public string? Id { get; set; }

        public string? Name { get; set; }
        public List<CT_TheLoai>? CT_TheLoais { get; set; }
    }
}
