using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Wed_Movie.Data;
using System.Collections.Generic;

namespace Wed_Movie.DAO
{
    public class UserDAO
    {
        public AppDbContext _dbContext { get; set; }
        public UserDAO()
        {
            _dbContext = new AppDbContext();
        }
        public AppUser user { get; set; }

        public string GetRoles()
        {
            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(_dbContext),null,null,null,null,null,null,null,null);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_dbContext), null, null, null, null);
            if (user != null)
            {
                return string.Join(", ", userManager.GetRolesAsync(user).Result);
            }
            return string.Empty;
        }

    }

    public class LoginDAO
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? password { get; set; }

        public bool remeberMe { get; set; }

        public string? returnUrl { get; set; }
    }
    public class RegisterDAO
    {
        [Required]
        public string? name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? confirmPassword { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",ErrorMessage = "Entered phone format is not valid.")]
        public string? phonenumber { get; set; }

        public string? returnUrl{ get; set; }

        public IList<AuthenticationScheme>? externalLogins { get; set; }
    }
}
