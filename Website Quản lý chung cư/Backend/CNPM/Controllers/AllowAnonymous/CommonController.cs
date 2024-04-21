using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CNPM.Service.Interfaces;
using CNPM.Core.Models;
using CNPM.Core.Utils;
namespace CNPM.Controllers.AllowAnonymous
{
    [ApiController]
    [Route(Constant.API_BASE)]
    public class CommonController : ControllerBase
    {
        private readonly IUserService _userService;
        public CommonController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] UserDto1004 userData)
        {
            return _userService.Authenticate(userData);
        }
    }
}