﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CNPM.Service.Interfaces;
using CNPM.Core.Models;
using CNPM.Core.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CNPM.Service.Implementations;
using CNPM.Core.Models.NhanKhau;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace CNPM.Controllers.Authorize
{
    [Authorize]
    [ApiController]
    [Route(Constant.API_BASE)]
    public class NhanKhauController : ControllerBase
    {
        private readonly INhanKhauService _nhanKhauService;
        public NhanKhauController(INhanKhauService nhanKhauService)
        {
            _nhanKhauService = nhanKhauService;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpGet("nhan-khau/danh-sach-nhan-khau")]
        public IActionResult GetListNhanKhau(int index, int limit)
        {
            return _nhanKhauService.GetListNhanKhau(index, limit);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpGet("nhan-khau/danh-sach-nhan-khau-chua-dang-ky-tam-vang")]
        public IActionResult GetListNhanKhauAlive(int index, int limit)
        {
            return _nhanKhauService.GetListNhanKhauAlive(index, limit);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpGet("nhan-khau/danh-sach-nhan-khau-chua-co-ho-khau")]
        public IActionResult GetListNhanKhauNotHaveHoKhau(int index, int limit)
        {
            return _nhanKhauService.GetListNhanKhauNotHaveHoKhau(index, limit);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpGet("nhan-khau/danh-sach-nhan-khau-trong-ho-khau")]
        public IActionResult GetListNhanKhauInHoKhau(string maHoKhau)
        {
            return _nhanKhauService.GetListNhanKhauInHoKhau(maHoKhau);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpGet("nhan-khau")]
        public IActionResult GetNhanKhau(int maNhanKhau)
        {
            return _nhanKhauService.GetNhanKhau(maNhanKhau);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpPost("nhan-khau")]
        public IActionResult CreateNhanKhau([FromBody] NhanKhauDto1000 nhanKhau)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(nhanKhau);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _nhanKhauService.CreateNhanKhau(token, nhanKhau);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpPut("nhan-khau")]
        public IActionResult UpdateNhanKhau([FromBody] NhanKhauDto1002 nhanKhau, int maNhanKhau)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(nhanKhau);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _nhanKhauService.UpdateNhanKhau(token, maNhanKhau, nhanKhau);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpDelete("nhan-khau")]
        public IActionResult DeleteNhanKhau(int maNhanKhau, int version)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _nhanKhauService.DeleteNhanKhau(maNhanKhau, token, version);
        }
    }
}