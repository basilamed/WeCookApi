namespace WeCook_Gateway.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public float Rating { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
