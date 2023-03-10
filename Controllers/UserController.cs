﻿using Arch.EntityFrameworkCore;
using HappyCamps_backend.Common;
using HappyCamps_backend.Context;
using HappyCamps_backend.DTOs;
using HappyCamps_backend.Helpers;
using HappyCamps_backend.Models;
using HappyCamps_backend.Services;
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
        private readonly IUserService userService;
        private readonly IValidateNewUser validateNewUser;
        private readonly IValidateUserLoginDTO validateUserLoginDTO;

        public UserController(IUserService userService, IValidateNewUser validateNewUser, IValidateUserLoginDTO validateUserLoginDTO)
        {
            this.userService = userService;
            this.validateNewUser = validateNewUser;
            this.validateUserLoginDTO = validateUserLoginDTO;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (!validateUserLoginDTO.IsValid(userLoginDto))
            {
                return BadRequest(new
                {
                    Message = "User is not valid."
                });
            }

            var user = userService.GetUserByEmail(userLoginDto.Email);

            if (user == null)
            {
                return BadRequest(new
                {
                    Message = "Email incorrect."
                });
            }

            if (!PasswordHasher.VerifyPassword(userLoginDto.Password, user.Password))
            {
                return BadRequest(new
                {
                    Message = "Password incorrect."
                });
            }

            var TokenizedUser = new TokenizedUser(user);

            TokenizedUser.Token = CreateJwtToken(user);

            return Ok(new
            {
                Token = TokenizedUser.Token,
                Message = "Login success."
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            var user = ToUser(userRegisterDTO);

            if (!await validateNewUser.IsValid(user))
            {
                return BadRequest(new
                {
                    Message = "Invalid new user or null fields."
                });
            }

            if (!await validateNewUser.IsUserUnique(user))
            {
                return BadRequest(new
                {
                    Message = "User with this credentials already exists."
                });
            }

            user.Password = PasswordHasher.HashPassword(user.Password);

            bool isUserSaved = userService.SaveNewUser(user);

            if (!isUserSaved)
            {
                return BadRequest(new
                {
                    Message = "Something went wrong when registering"
                });
            }

            return Ok(new
            {
                message = "Register successful."
            });
        }

        private string CreateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = System.Text.Encoding.ASCII.GetBytes("veryverysecret.....");

            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,user.RoleType.ToString()),
                new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }

        private User ToUser(UserRegisterDTO userRegisterDTO)
        {
            User user = new User
            {
                FirstName = userRegisterDTO.FirstName,
                LastName = userRegisterDTO.LastName,
                Email = userRegisterDTO.Email,
                Password = userRegisterDTO.Password,
                BirthDate = userRegisterDTO.BirthDate,
                PhoneNumber = userRegisterDTO.PhoneNumber,
                City = userRegisterDTO.City,
                Instagram = userRegisterDTO.Instagram,
                RoleType = SetRole(userRegisterDTO.Role),
                Points = 0,
                Accepted = userRegisterDTO.Role == Role.VOLUNTEER.ToString() ? true:false
            };

            return user;
        }

        private Role SetRole(string roleFromFrontend)
        {
            switch (roleFromFrontend)
            {
                case "ADMIN":
                    return Role.ADMIN;
                case "VOLUNTEER":
                    return Role.VOLUNTEER;
                default:
                    return Role.ORGANIZER;
            }
        }
    }
}
