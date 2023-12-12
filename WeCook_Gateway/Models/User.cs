using Microsoft.AspNetCore.Identity;

namespace WeCook_Gateway.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public string? Image { get; set; }
        public bool Approved { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        public bool RequestedToBeChef { get; set; } = false;
        public List<Favorite> FavoriteRecipes { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
