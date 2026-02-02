using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Solar.Dtos;
using BCrypt.Net;

namespace Solar
{
    [ApiController]

    [Route("api/hello")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public IActionResult SomeRandomAction()
        {
            return Ok("Por que têm tanto erro");   
        }
    }

    [ApiController]
    public class PostUser : ControllerBase
    {
        private readonly UserService _userService;

        public PostUser(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("api/helloPost")]
        public async Task<IActionResult> CreateUserDto([FromBody] CreateUserDto userDto)
        {
            try
            {                
                if (userDto == null)
                {
                    return BadRequest();
                }

                var user = await _userService.CreateUserAsync(userDto);

                return Created("", new
                {
                    user.Id,
                    user.Name,
                    user.Email
                });
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
    [ApiController]
    public class GetAllUsers : ControllerBase
    {
        private readonly UserService _userService;

        public GetAllUsers(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("api/getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}