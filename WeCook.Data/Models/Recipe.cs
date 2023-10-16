using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeCook.Data.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public string PreporationTime {  get; set; }
        public string? Image { get; set; }
        public DateTime PostingDate { get; set; }
        public string ChefId { get; set; }
        public User Chef { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Favorite> FavoritedByUsers { get; set; }
    }

}
