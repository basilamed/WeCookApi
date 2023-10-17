using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using WeCook.Data;
using WeCook.Data.Models;
using WeCook_Api.DTOs;

namespace WeCook_Api.Services
{
    public class RecipeService
    {
        private readonly AppDbContext context;
        
        public RecipeService(AppDbContext context)
        {
            this.context = context;
        }

        public List<Recipe> GetAll()
        {
            var lista = context.Recipes.ToList();
            if (lista == null)
            {
                throw new Exception("No recipes");
            }
            return lista;
        }
        public Recipe GetRecipeById(int id)
        {
            var recipe = context.Recipes.FirstOrDefault(x => x.Id == id);
            if (recipe == null)
            {
                throw new Exception("Recipe not found");

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
