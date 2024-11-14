using HospitalManagment.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using HospitalManagment.WebApi.Models;
using RegisterRequest = HospitalManagment.WebApi.Models.RegisterRequest;
using HospitalManagment.Business.Operations.User.Dtos;
using HospitalManagment.Business.Operations.User;
using LoginRequest = HospitalManagment.WebApi.Models.LoginRequest;
using HospitalManagment.WebApi.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace HospitalManagment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addUserDto = new AddUserDto
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.Lastname,
                Password = request.Password

            };

            var result = await _userService.AddUser(addUserDto);

            if (result.IsSucceed)
                return Ok();
            else
                return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result  = _userService.LoginUser(new LoginUserDto {  Email = request.Email, Password = request.Password });

            if(!result.IsSucceed)
                return BadRequest(result.Message);


            var user = result.Data;

            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id = user.Id,
                Email= user.Email,
                FirstName= user.FirstName,
                LastName= user.LastName,
                Role = user.Role,
                SecretKey = configuration["Jwt:SecretKey"],
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)
            });

            return Ok(new LoginResponse
            {
                Message = "Login completed successfully",
                Token = token
            });
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetMyUser()
        {
            return Ok();
        }
    }
}
