using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using WeCook_Api.DTOs;
using WeCook_Api.Services;

namespace WeCook_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly FavoriteService favoriteService;

        public FavoritesController(FavoriteService favoriteService)
        {
            this.favoriteService = favoriteService;
        }

        [HttpGet("get-favorites/{userId}")]
        public IActionResult GetFavoritesByUser(string userId)
        {
            try
            {
                var res = favoriteService.GetFavoritesByUser(userId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-favorite")]
        public IActionResult AddFavorite([FromBody] AddFavoriteDto favorite)
        {
            try
            {
                var res = favoriteService.AddFavorite(favorite);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-favorite/{userid}/{recipeid}")]
        public IActionResult DeleteFavorite(string userid, int recipeid)
        {
            AddFavoriteDto favorite = new AddFavoriteDto();
            favorite = new AddFavoriteDto
            {
                RecipeId = recipeid,
                UserId = userid
            };
            try
            {
                var res = favoriteService.DeleteFavorite(favorite);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
