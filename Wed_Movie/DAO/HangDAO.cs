using System.ComponentModel.DataAnnotations;

namespace Wed_Movie.DAO
{
    public class HangDAO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên hãng phim!")]
        [Display(Name = "Tên Hãng")]
        public string? Name { get; set; }
    }
}
