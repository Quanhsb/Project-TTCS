using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CNPM.Service.Interfaces;
using CNPM.Core.Models;
using CNPM.Core.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CNPM.Service.Implementations;
using CNPM.Core.Models.NhanKhau;
using Microsoft.EntityFrameworkCore.Diagnostics;
using CNPM.Core.Models.HoKhau;
using CNPM.Core.Models.Xe;
namespace CNPM.Controllers.Authorize
{
    //[VerifyToken]
    [Authorize]
    [ApiController]
    [Route(Constant.API_BASE)]
    public class HoKhauController : ControllerBase
    {
        private readonly IHoKhauService _hoKhauService;
        public HoKhauController(IHoKhauService hoKhauService)
        {
            _hoKhauService = hoKhauService;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpGet("ho-khau/danh-sach-ho-khau")]
        public IActionResult GetListHoKhau(int index, int limit)
        {
            return _hoKhauService.GetListHoKhau(index, limit);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpGet("ho-khau")]
        public IActionResult GetHoKhau(string maHoKhau)
        {
            return _hoKhauService.GetHoKhau(maHoKhau);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpPost("ho-khau")]
        public IActionResult CreateHoKhau([FromBody] HoKhauDto1000 hoKhau)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(hoKhau);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _hoKhauService.CreateHoKhau(token, hoKhau);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpPut("ho-khau")]
        public IActionResult UpdateHoKhau([FromBody] HoKhauDto1002 hoKhau, string maHoKhau)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(hoKhau);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _hoKhauService.UpdateHoKhau(token, maHoKhau, hoKhau);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpPost("ho-khau/add-phong-to-ho-khau")]
        public IActionResult AddPhongToHoKhau(string maHoKhau, int maPhong)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _hoKhauService.AddPhongToHoKhau(token, maHoKhau, maPhong);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpPost("ho-khau/remove-phong-from-ho-khau")]
        public IActionResult RemovePhongFromHoKhau(string maHoKhau)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _hoKhauService.RemovePhongFromHoKhau(token, maHoKhau);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpPost("ho-khau/add-xe")]
        public IActionResult AddXeToHoKhau([FromBody] XeDto1000 xe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(xe);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _hoKhauService.AddXeToHoKhau(token, xe);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpPost("ho-khau/update-xe")]
        public IActionResult UpdateXe(int maXe, [FromBody] XeDto1002 xe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(xe);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _hoKhauService.UpdateXe(token, maXe, xe);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpPost("ho-khau/remove-xe")]
        public IActionResult RemoveXeFromHoKhau(int maXe)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _hoKhauService.RemoveXeFromHoKhau(token, maXe);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpDelete("ho-khau")]
        public IActionResult DeleteHoKhau(string maHoKhau, int version)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _hoKhauService.DeleteHoKhau(token, maHoKhau, version);
        }
    }
}