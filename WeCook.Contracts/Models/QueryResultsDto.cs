using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeCook.Contracts.Models
{
    public class QueryResultsDto<T> where T : class
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
