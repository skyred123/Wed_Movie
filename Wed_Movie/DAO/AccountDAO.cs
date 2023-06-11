using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using MovieModel.Config;
using Wed_Movie.Entities;

namespace Wed_Movie.DAO
{
    public class UserDAO
    {
        public UserDAO()
        {
        }
        public ApplicationUser user { get; set; }

    }

    public class LoginDAO
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} phải dài ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string? password { get; set; }
        [Display(Name = "lưu đăng nhập của tôi")]
        public bool remeberMe { get; set; }

        public string? returnUrl { get; set; }
    }
    public class RegisterDAO
    {
        //[Required]
        public string? name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} phải dài ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string? password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]
        public string? confirmPassword { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Số Điện Thoại")]
        [Required(ErrorMessage = "Số điện thoại bắt buộc!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",ErrorMessage = "Định dạng điện thoại đã nhập không hợp lệ.")]
        public string? phonenumber { get; set; }

        public string? returnUrl{ get; set; }

        public IList<AuthenticationScheme>? externalLogins { get; set; }
    }
    public class ProcessEmailConfirmDAO
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? email { get; set; }
    }


}
