using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration configuration;

        public LoginController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public object UserLogin(User user)
        {
            if (user.Username == "svili.svili" && user.Password == "okupasesiNamePantago98")
            {
                var claims = new[]
                {
                    new Claim("Username", user.Username)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(5), signingCredentials: creds);
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }

            return Unauthorized("Username or password is not valid");
        }
    }
}
