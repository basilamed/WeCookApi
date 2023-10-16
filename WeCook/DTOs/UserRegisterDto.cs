using System.ComponentModel.DataAnnotations;

namespace WeCook_Api.DTOs
{
    public class UserRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
