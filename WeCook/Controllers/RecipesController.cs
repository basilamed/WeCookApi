using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeCook.Contracts.Models;
using WeCook.Data.Models;
using WeCook_Api.DTOs;
using WeCook_Api.Services;

namespace WeCook_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipeService recipeService;

        public RecipesController(RecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipes([FromQuery] RecipeQuery dto)
        {
            try
            {
                var recipes = await recipeService.GetAll(dto);
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("rating/{id}")]
        public IActionResult GetRating(int id)
        {
            try
            {
                double rating = recipeService.GetRating(id);
                return Ok(rating);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetRecipeById(int id)
        {
            try
            {
                Recipe recipe = recipeService.GetRecipeById(id);
                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("chef/{id}")]
        public IActionResult GetRecipesByChefId(string id)
        {
            try
            {
                List<Recipe> recipe = recipeService.GetRecipesByChefId(id);
                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddRecipe([FromBody] AddRecipeDto recipeDto)
        {
            try
            {
                Recipe recipe = recipeService.AddRecipe(recipeDto);
                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRecipe(int id, [FromBody] UpdateRecipeDto recipeDto)
        {
            try
            {
                Recipe recipe = recipeService.UpdateRecipe(id, recipeDto);
                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            try
            {
                bool result = recipeService.DeleteRecipe(id);
                if (result)
                {
                    return Ok("Recipe deleted successfully.");
                }
                return NotFound("Recipe not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
