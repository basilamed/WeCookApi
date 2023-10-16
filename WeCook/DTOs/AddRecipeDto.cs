using System.ComponentModel.DataAnnotations;

namespace WeCook_Api.DTOs
{
    public class AddRecipeDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Ingredients { get; set; }
        [Required]
        public string Instructions { get; set; }
        [Required]
        public string PreporationTime { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string ChefId { get; set; }
    }
}
