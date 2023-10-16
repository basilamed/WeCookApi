using Microsoft.EntityFrameworkCore;
using WeCook.Data;
using WeCook.Data.Models;
using WeCook_Api.DTOs;

namespace WeCook_Api.Services
{
    public class FavoriteService
    {
        private readonly AppDbContext context;

        public FavoriteService(AppDbContext context)
        {
            this.context = context;
        }

        public User GetFavoritesByUser(string userId)
        {
            var lista = context.Users.Where(u => u.Id == userId).Include(x => x.FavoriteRecipes).FirstOrDefault();
            if (lista == null)
            {
                throw new Exception("No comments by user");
            }
            return lista;
        }
        public Favorite AddFavorite(AddFavoriteDto dto)
        {
            var existingFavorite = context.Favorites
                .FirstOrDefault(favorite => favorite.UserId == dto.UserId && favorite.RecipeId == dto.RecipeId);

            if (existingFavorite != null)
            {
                throw new Exception("This recipe is already in your favorites.");
            }

            var favorite = new Favorite
            {
                RecipeId = dto.RecipeId,
                UserId = dto.UserId,
            };

            context.Favorites.Add(favorite);
            context.SaveChanges();

            return favorite;
        }

        //public bool DeleteFavorite(string userId, int recipeId)
        //{
        //    var favorite = context.Favorites.FirstOrDefault(f => f.UserId == userId && f.RecipeId == recipeId);

        //    if (favorite == null)
        //    {
        //        throw new Exception("Favorite not found");
        //    }

        //    context.Favorites.Remove(favorite);

        //    try
        //    {
        //        context.SaveChanges();
        //        return true;
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        throw new Exception("An error occurred while deleting the favorite.", ex);
        //    }
        //}

        public bool DeleteFavorite(int id)
        {
            var favorite = context.Favorites.FirstOrDefault(f => f.Id == id);

            if (favorite == null)
            {
                throw new Exception("Favorite not found");
            }

            context.Favorites.Remove(favorite);

            try
            {
                context.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while deleting the favorite.", ex);
            }
        }



    }
}
