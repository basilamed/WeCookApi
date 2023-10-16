using System.ComponentModel.DataAnnotations;

namespace WeCook_Api.DTOs
{
    public class AddCommentDto
    {
        [Required]
        public float Rating { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int RecipeId { get; set; }
    }
}
