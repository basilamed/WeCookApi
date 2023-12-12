using System.ComponentModel.DataAnnotations;

namespace WeCook_Gateway.Models.DTOs
{
    public class AddComment
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
