using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Wed_Movie.DAO
{
    public class TheLoaiDAO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên thể loại!")]
        [Display(Name = "Tên Thể Loại")]
        public string? Name { get; set; }
    }
}
