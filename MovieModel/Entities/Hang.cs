using System.ComponentModel.DataAnnotations;

namespace Wed_Movie.Entities
{
    public class Hang
    {
        [MaxLength(128)]
        public string? Id { get; set; }

        public string? Name { get; set; }


        public List<CT_Hang>? CT_Hang { get; set; }
    }
}
