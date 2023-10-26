using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WeCook.Contracts.Models
{
    public static class QueryExtension
    {
        //za sortiranje
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, RecipeQuery recipeQuery, Dictionary<string, Expression<Func<T, object>>> sortCol)
        {
            if (string.IsNullOrEmpty(recipeQuery.SortBy) || !sortCol.ContainsKey(recipeQuery.SortBy))
            {
                return query;
            }

            bool isSortAscending = recipeQuery.IsSortAscending ?? false;

            if (isSortAscending)
            {
                query = query.OrderBy(sortCol[recipeQuery.SortBy]);
            }
            else
            {
                query = query.OrderByDescending(sortCol[recipeQuery.SortBy]);
            }

            return query;
        }



        //paginacija
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, RecipeQuery recipeQuery)
        {
            int page = recipeQuery.Page ?? 1;
            int pageSize = recipeQuery.PageSize ?? 10;

            if (page <= 0)
            {
                page = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
