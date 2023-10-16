using System.ComponentModel.DataAnnotations;

namespace WeCook_Api.DTOs
{
    public class UserChangePasswordDto
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required, Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
