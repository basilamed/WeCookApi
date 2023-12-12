using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeCook.Data.Models;
using WeCook_Api.DTOs;
using WeCook_Api.Services;

namespace WeCook_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentService commentService;

        public CommentsController(CommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet("user/{id}")]
        public IActionResult GetAllCommentsByUserId(string id)
        {
            try
            {
                List<Comment> comments = commentService.GetAllByUserId(id);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("recipe/{id}")]
        public IActionResult GetAllCommentsByRecipeId(int id)
        {
            try
            {
                List<Comment> comments = commentService.GetAllByRecipeId(id);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCommentById(int id)
        {
            try
            {
                Comment comment = commentService.GetById(id);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddComment([FromBody] AddCommentDto commentDto)
        {
            try
            {
                Comment comment = commentService.AddComment(commentDto);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            try
            {
                var result = commentService.DeleteComment(id);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
