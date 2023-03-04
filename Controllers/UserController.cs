using Arch.EntityFrameworkCore;
using HappyCamps_backend.Context;
using HappyCamps_backend.DTOs;
using HappyCamps_backend.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HappyCamps_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly HappyCampsDataContext _authContext;
        public UserController(HappyCampsDataContext authContext)
        {
            this._authContext = authContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if(userLoginDto == null)
            {
                return BadRequest();
            }

            var user = _authContext.Users
                .FirstOrDefault(x => x.Email == userLoginDto.Email && x.Password == userLoginDto.Password);

            if(user == null)
            {
                return NotFound(new
                {
                    message = "user not found"
                }) ;
            }

            return Ok(new
            {
                Message = "login success"
            });
        }

        public async Task<IActionResult> Register([FromBody] UserLoginDto userLoginDto)
        {
            if (userLoginDto == null)
            {
                return BadRequest();
            }
            userLoginDto.Password = PasswordHasher.HashPassword(userLoginDto.Password);
            //_authContext.Users.Add(userLoginDto);
            //_authContext.SaveChanges();
            return Ok(new
            {
                message = "user registered"
            });
        }

    }
}
