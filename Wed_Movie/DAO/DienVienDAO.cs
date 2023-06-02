using System.ComponentModel.DataAnnotations;

namespace Wed_Movie.DAO
{
    public class DienVienDAO
    {

        public string? Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên diễn viên!")]
        [Display(Name = "Tên diễn viên")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày sinh!")]
        public DateTime? Birthday { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập quốc gia  .")]
        public string? Nationality { get; set; }

        [EnumDataType(typeof(SexEnum), ErrorMessage = "Giới tính không hợp lệ.")]
        public string? Sex { get; set; }

        public IFormFile? Image { get; set; }

        private enum SexEnum
        {
            [Display(Name = "Nam")]
            Male,

            [Display(Name = "Nữ")]
            Female,

            [Display(Name = "Khác")]
            Other
        }
    }
}
