using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using WeCook.Data;
using WeCook.Data.Models;
using WeCook_Api.DTOs;
using WeCook.Contracts.Models;
using Azure.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace WeCook_Api.Services
{
    public class RecipeService
    {
        private readonly AppDbContext context;
        
        public RecipeService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<QueryResultsDto<Recipe>> GetAll([FromQuery] RecipeQuery request)
        {
            var lista = context.Recipes
                .Include(c => c.Chef)
                .AsQueryable();

            if (lista == null)
            {
                throw new Exception("No recipes");
            }
            if (request.DateOfPosting.HasValue)
            {
                DateTime dateOfPosting = request.DateOfPosting.Value.Date;

                lista = lista.Where(a => a.PostingDate.Date == dateOfPosting);
            }
            if (request.TimeToCook.HasValue)
            {
                lista = lista.Where(a => a.PreporationTime == request.TimeToCook);
            }
            if (request.Temperature.HasValue)
            {
                lista = lista.Where(a => a.Temperature == request.Temperature);
            }
            var sortColumns = new Dictionary<string, Expression<Func<Recipe, object>>>()
            {
                ["PostingDate"] = c => c.PostingDate
            };
            Expression<Func<Recipe, object>> selectedColumn = null;
            if (!string.IsNullOrEmpty(request.SortBy) && sortColumns.ContainsKey(request.SortBy))
            {
                selectedColumn = sortColumns[request.SortBy];
            }
            var totalCount = await lista.CountAsync();

            lista = lista.ApplySorting(request, sortColumns);
            lista = lista.ApplyPaging(request);

            var list = lista.ToList();
            QueryResultsDto<Recipe> result = new QueryResultsDto<Recipe>
            {
                TotalItems = totalCount,
                Items = list
            };

            return result;
        }

        public Recipe GetRecipeById(int id)
        {
            var recipe = context.Recipes.Include(u=> u.Chef).Include(c => c.Comments).ThenInclude(u => u.User).FirstOrDefault(x => x.Id == id);
            if (recipe == null)
            {
                throw new Exception("Recipe not found");

            }
            return recipe;

        }
        public List<Recipe> GetRecipesByChefId(string id)
        {
            var recipe = context.Recipes.Where(x => x.ChefId == id).ToList();
            if (recipe == null)
            {
                throw new Exception("Recipes not found");

            }
            return recipe;

        }

        public Recipe AddRecipe(AddRecipeDto dto)
        {
            Recipe recipe = new Recipe();
            recipe = new Recipe {
                Title = dto.Title,
                Ingredients = dto.Ingredients,
                Instructions = dto.Instructions,
                Image = dto.Image,
                PreporationTime = dto.PreporationTime,
                PostingDate = DateTime.UtcNow,
                ChefId = dto.ChefId,
                Taste = dto.Taste,
                Temperature = dto.Temperature,
            };
            context.Recipes.Add(recipe);
            context.SaveChanges();
            return recipe;
        }
        public Recipe UpdateRecipe(int id, UpdateRecipeDto dto)
        {
            var recipe = context.Recipes.FirstOrDefault(c => c.Id == id);
            if (recipe == null)
            {
                throw new Exception("Recipe not found");
            }
            else
            {
                recipe.Title = dto.Title;
                recipe.Ingredients = dto.Ingredients;
                recipe.Instructions = dto.Instructions;
                recipe.Image = dto.Image;
                recipe.PreporationTime = dto.PreporationTime;
                recipe.Taste = dto.Taste;
                recipe.Temperature = dto.Temperature;

            }
            context.SaveChanges();
            return recipe;
        }

        public bool DeleteRecipe(int id)
        {
            var recipe = context.Recipes.FirstOrDefault(r => r.Id == id);

            if (recipe == null)
            {
                throw new Exception("Recipe not found");
            }

            context.Recipes.Remove(recipe);

            try
            {
                context.SaveChanges();
                return true; 
            }
            catch (DbUpdateException ex)
            {
               throw new Exception("An error occurred while deleting the recipe.", ex);
            }
        }

    }
}
