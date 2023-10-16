using System.ComponentModel.DataAnnotations;

namespace WeCook_Api.DTOs
{
    public class AddFavoriteDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int RecipeId { get; set; }
    }
}
