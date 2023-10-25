using Microsoft.AspNetCore.Identity;
using WeCook.Data.Models;
using WeCook.Data;
using System.Web;
using WeCook_Api.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace WeCook_Api.Services
{
    public class UserService
    {

        private readonly UserManager<User> userManager;
        private IConfiguration configuration;
        private readonly AppDbContext context;
        public UserService(UserManager<User> userManager, IConfiguration configuration, AppDbContext context)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.context = context;
        }

        public async Task<bool> Register(UserRegisterDto user)
        {
            var u = await userManager.FindByNameAsync(user.UserName);
            if (u != null)
            {
                throw new Exception("User already exists");
            }
            User us = new User();
            if (user.RoleId == 2)
            {
                us = new User
                {
                    UserName = user.UserName,
                    Email = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    RoleId = user.RoleId,
                    Approved = true
                };
            }
            else if (user.RoleId != 2)
            {
                us = new User
                {
                    UserName = user.UserName,
                    Email = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    RoleId = user.RoleId,
                    Approved = false
                };

            }
            var result = await userManager.CreateAsync(us, user.Password);
            if (result.Succeeded)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(us);
                var encodedToken = HttpUtility.UrlEncode(token);
                var htmlContent = $"<h1>Welcome to We Cook</h1>" +
                    $"<h3>Please click " +
                 $"<a href=\"{configuration.GetSection("ClientAppUrl").Value}/verify/{us.UserName}/{us.SecurityStamp}\">here</a>" +
                 $" to confirm your account</h3>";
                SendEmail(us, "Verify your account", htmlContent).Wait();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> Verify(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                if (user.SecurityStamp == token)
                {
                    user.IsEmailConfirmed = true;
                    await context.SaveChangesAsync();
                    await userManager.UpdateAsync(user);
                    return true;
                }
                else
                {
                    throw new Exception("bad request");
                }
            }
            else
            {
                throw new Exception("not found");
            }
        }

        public async Task<object> Login(UserLoginDto user)
        {
            var u = await userManager.FindByNameAsync(user.UserName);
            if (u == null)
            {
                throw new Exception("User do not exists");
            }

            if (!u.IsEmailConfirmed)
            {
                throw new Exception("Email is not confirmed");
            }

            if (await userManager.CheckPasswordAsync(u, user.Password))
            {
                var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("78fUjkyzfLz56gTq"));
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256)
                    );
                var toReturn = new JwtSecurityTokenHandler().WriteToken(token);
                var obj = new
                {
                    expires = DateTime.Now.AddHours(2),
                    token = toReturn,
                    user = u
                };
                return obj;
            }
            else
            {
                throw new Exception("Username and password not match");
            }
        }
        public async Task SendEmail(User u, string subject, string htmlContent)
        {

        }
        public async Task<bool> DeleteUser(string userId)
        {
            var u = await userManager.FindByIdAsync(userId);
            if (u == null)
            {
                throw new Exception("User not found");
            }
            var result = await userManager.DeleteAsync(u);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }

        public User GetUser(string userId)
        {
            var u = context.Users.Include(c=> c.FavoriteRecipes).FirstOrDefault( m => m.Id == userId);
            if (u == null)
            {
                throw new Exception("User not found");
            }
            return u;
        }

        public List<User> GetAllUsers()
        {
            var u = context.Users.Where(u => u.Approved == true && u.RoleId != 1).ToList();
            if (u == null)
            {
                throw new Exception("No users");
            }
            return u;
        }

        public List<User> GetAllUnapprovedUsers()
        {
            var u = context.Users.Where(a=> a.Approved == false).ToList();
            if (u == null)
            {
                throw new Exception("No unapproved users");
            }
            return u;
        }
        public User ApproveUser(string id)
        {
            var u = context.Users.FirstOrDefault(u => u.Id == id);
            if (u == null)
            {
                throw new Exception("User does not exist");
            }
            if (u.Approved == true)
            {
                throw new Exception("User already approved");
            }
            u.Approved = true;
            context.SaveChanges();
            return u;
        }
        public async Task<User> UpdateUser(string userId, UserUpdateDto user)
        {
            var u = await userManager.FindByIdAsync(userId);
            if (u == null)
            {
                throw new Exception("User not found");
            }
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.Image = user.Image;
            var result = await userManager.UpdateAsync(u);
            if (result.Succeeded)
            {
                return u;
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }
        public async Task<bool> ChangePassword(string userId, UserChangePasswordDto user)
        {
            var u = await userManager.FindByIdAsync(userId);
            if (u == null)
            {
                throw new Exception("User not found");
            }
            var result = await userManager.ChangePasswordAsync(u, user.OldPassword, user.NewPassword);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }
    }
}
