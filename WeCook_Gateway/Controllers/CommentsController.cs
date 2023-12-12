using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WeCook_Gateway.Models;
using WeCook_Gateway.Models.DTOs;


namespace WeCook_Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly HttpClient http;
        private readonly Urls urls;
        public CommentsController(HttpClient http, IOptions<Urls> urls)
        {
            this.http = http;
            this.urls = urls.Value;
        }

        [HttpGet("get-all-comments-by-user/{id}")]
        public async Task<IActionResult> GetByUserId(string id)
        {
            //implement endpoint by calling the microservice
            var response = http.GetAsync(urls.Comments + "/api/Comments/user/" + id).Result;
            response.EnsureSuccessStatusCode();
            var context = await response.Content.ReadAsStringAsync();
            var comments = JsonConvert.DeserializeObject<List<Comment>>(context);

            return Ok(comments);
        }

        //get all comments by recipe id
        [HttpGet("get-all-comments-by-recipe/{id}")]

        public async Task<IActionResult> GetByRecipeId(int id)
        {
            //implement endpoint by calling the microservice
            var response = http.GetAsync(urls.Comments + "/api/Comments/recipe/" + id).Result;
            response.EnsureSuccessStatusCode();
            var context = await response.Content.ReadAsStringAsync();
            var comments = JsonConvert.DeserializeObject<List<Comment>>(context);

            return Ok(comments);
        }

        //get comment by id
        [HttpGet("get-comment-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //implement endpoint by calling the microservice
            var response = http.GetAsync(urls.Comments + "/api/Comments/" + id).Result;
            response.EnsureSuccessStatusCode();
            var context = await response.Content.ReadAsStringAsync();
            var comment = JsonConvert.DeserializeObject<Comment>(context);

            return Ok(comment);
        }

        //create comment
        [HttpPost("create-comment")]

        public async Task<IActionResult> CreateComment(AddComment comment)
        {
            //implement endpoint by calling the microservice
            var response = await http.PostAsync(urls.Comments + "/api/Comments", new StringContent(JsonConvert.SerializeObject(comment), System.Text.Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var context = await response.Content.ReadAsStringAsync();
            var commentCreated = JsonConvert.DeserializeObject<Comment>(context);

            return Ok(commentCreated);
        }

        //delete comment
        [HttpDelete("delete-comment/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            //implement endpoint by calling the microservice
            var response = await http.DeleteAsync(urls.Comments + "/api/Comments/" + id);
            response.EnsureSuccessStatusCode();
            var context = await response.Content.ReadAsStringAsync();
            var commentDeleted = JsonConvert.DeserializeObject<Comment>(context);

            return Ok(commentDeleted);
        }
    }
}
