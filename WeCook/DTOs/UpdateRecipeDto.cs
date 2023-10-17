namespace WeCook_Api.DTOs
{
    public class UpdateRecipeDto
    {
        public string Title { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public string PreporationTime { get; set; }
        public string Image { get; set; }
        public bool Taste { get; set; }
        public string Temperature { get; set; }
    }
}
