using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeCook.Data.Models
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public string? Image { get; set; }
        public bool Approved { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        public List<Favorite> FavoriteRecipes { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
