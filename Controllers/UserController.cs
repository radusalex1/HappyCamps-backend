using Arch.EntityFrameworkCore;
using HappyCamps_backend.Context;
using HappyCamps_backend.DTOs;
using HappyCamps_backend.Helpers;
using HappyCamps_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HappyCamps_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly HappyCampsDataContext _authContext;
        public UserController(HappyCampsDataContext authContext)
        {
            this._authContext = authContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (userLoginDto == null)
            {
                return BadRequest();
            }

            var user = _authContext.Users
                .FirstOrDefault(x => x.Email == userLoginDto.Email);


            if (!PasswordHasher.VerifyPassword(userLoginDto.Password, user.Password))
            {
                if (user == null)
                {
                    return NotFound(new
                    {
                        message = "Password incorrect"
                    });
                }
            }

            var TokenizedUser = new TokenizedUser(user);

            TokenizedUser.Token = CreateJwtToken(user);

            return Ok(new
            {
                Token = TokenizedUser.Token,
                Message = "login success"
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            user.Password = PasswordHasher.HashPassword(user.Password);
            _authContext.Users.Add(user);
            _authContext.SaveChanges();
            return Ok(new
            {
                message = "user registered"
            });
        }

        private string CreateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Name,$"{user.Name} {user.LastName}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }

        [Authorize]
        [HttpGet("/GetAll")]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            return Ok( _authContext.Users.ToList());
        }

    }
}
