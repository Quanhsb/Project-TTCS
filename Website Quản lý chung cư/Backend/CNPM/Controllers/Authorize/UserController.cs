using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CNPM.Service.Interfaces;
using CNPM.Core.Models;
using CNPM.Core.Utils;
using CNPM.Service.Implementations;
namespace CNPM.Controllers.Authorize
{
    [Authorize]
    [ApiController]
    [Route(Constant.API_BASE)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager + ", " + Constant.Stocker)]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _userService.Logout(accessToken);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager + ", " + Constant.Stocker)]
        [HttpGet("verify-token")]
        public IActionResult VerifyToken()
        {
            var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _userService.VerifyToken(jwt);
        }
        [HttpGet("get-list-permissions")]
        [AllowAnonymous]
        public IActionResult GetListPermissions()
        {
            return _userService.GetListPermissions();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager + ", " + Constant.Stocker)]
        [HttpGet("user")]
        public IActionResult GetUser(string userName)
        {
            return _userService.GetUser(userName);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator)]
        [HttpPost("user")]
        public IActionResult CreateUser([FromBody] UserDto1005 user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(user);
            }
            return _userService.CreateUser(user);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator)]
        [HttpPut("user")]
        public IActionResult UpdateUser([FromBody] UserDto1006 newUserData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(newUserData);
            }
            return _userService.UpdateUser(newUserData);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator)]
        [HttpDelete("user")]
        public IActionResult DeleteUser([FromBody] UserDto1007 user)
        {
            return _userService.DeleteUser(user);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager + ", " + Constant.Stocker)]
        [HttpGet("get-all-users")]
        public IActionResult GetAllUsers()
        {
            return _userService.GetAllUsers();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager + ", " + Constant.Stocker)]
        [HttpPut("change-password")]
        public IActionResult ChangePassWord([FromBody] UserDto1000 userData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(userData);
            }
            return _userService.ChangePassWord(userData);
        }
    }
}