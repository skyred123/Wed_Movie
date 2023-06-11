using System.ComponentModel.DataAnnotations;

namespace Wed_Movie.Entities
{
    public class DienVien
    {
        [StringLength(128)]
        public string? Id { get; set; }
        public string? Name { get; set; }

        public DateTime? Birthday { get; set; }

        public string? Nationality { get; set; }

        public string? Sex { get; set; }

        public string? Image { get; set; }

        public List<CT_DienVien>? CT_DienViens { get; set; }
    }
}
