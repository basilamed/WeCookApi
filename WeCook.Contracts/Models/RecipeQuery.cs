using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeCook.Contracts.Models
{
    public class RecipeQuery
    {
        public string? SortBy { get; set; }
        public bool? IsSortAscending { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public DateTime? DateOfPosting { get; set; }
        public int? TimeToCook { get; set; }
        public int? Temperature {  get; set; }
        public string? Ingredients { get; set; }
    }
}
