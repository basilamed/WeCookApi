using Microsoft.EntityFrameworkCore;
using WeCook.Data;
using WeCook.Data.Models;
using WeCook_Api.DTOs;

namespace WeCook_Api.Services
{
    public class CommentService
    {

        private readonly AppDbContext context;

        public CommentService(AppDbContext context)
        {
            this.context = context;
        }

        public List<Comment> GetAllByUserId(string id)
        {
            var lista = context.Comments.Where(u => u.UserId == id).ToList();
            if (lista == null)
            {
                throw new Exception("No comments by user");
            }
            return lista;
        }

        public List<Comment> GetAllByRecipeId(int id)
        {
            var lista = context.Comments.Where(u => u.RecipeId == id).ToList();
            if (lista == null)
            {
                throw new Exception("No comments on recipe");
            }
            return lista;
        }
        public Comment GetById(int id)
        {
            var comment = context.Comments.FirstOrDefault(u => u.Id == id);
            if (comment == null)
            {
                throw new Exception("Comment not found");                                                                                                               
            } 
            return comment;
        }
        public Comment AddComment ( AddCommentDto dto )
        {
            Comment c = new Comment();
            c = new Comment
            {
                Rating = dto.Rating,
                Description = dto.Description,
                UserId  = dto.UserId,
                RecipeId    = dto.RecipeId,
                Created = DateTime.UtcNow
            };
            context.Comments.Add(c);
            context.SaveChanges();
            return c;

        }
        public Comment DeleteComment(int id)
        {
            var c = context.Comments.FirstOrDefault(r => r.Id == id);

            if (c == null)
            {
                throw new Exception("Comment not found");
            }

            context.Comments.Remove(c);

            try
            {
                context.SaveChanges();
                return c;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while deleting the comment.", ex);
            }
        }
    }
}
