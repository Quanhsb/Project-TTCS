﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CNPM.Service.Interfaces;
using CNPM.Core.Models;
using CNPM.Core.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CNPM.Service.Implementations;
using CNPM.Core.Models.KhoanThu;
using Microsoft.EntityFrameworkCore.Diagnostics;
using CNPM.Core.Models.HoaDon;
namespace CNPM.Controllers.Authorize
{
    [Authorize]
    [ApiController]
    [Route(Constant.API_BASE)]
    public class KhoanThuController : ControllerBase
    {
        private readonly IKhoanThuService _khoanThuService;
        public KhoanThuController(IKhoanThuService khoanThuService)
        {
            _khoanThuService = khoanThuService;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager + ", " + Constant.Stocker)]
        [HttpGet("khoan-thu/danh-sach-khoan-thu")]
        public IActionResult GetListKhoanThu(int index, int limit)
        {
            return _khoanThuService.GetListKhoanThu(index, limit);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager + ", " + Constant.Stocker)]
        [HttpGet("khoan-thu")]
        public IActionResult GetKhoanThu(int maKhoanThu)
        {
            return _khoanThuService.GetKhoanThuTheoHo(maKhoanThu);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager + ", " + Constant.Stocker)]
        [HttpGet("khoan-thu-theo-ho")]
        public IActionResult GetKhoanThuTheoHo(string maHoKhau)
        {
            return _khoanThuService.GetCacKhoanThuDaNopCuaHo(maHoKhau);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager + ", " + Constant.Stocker)]
        [HttpPost("khoan-thu")]
        public IActionResult CreateKhoanThu([FromBody] KhoanThuDto1000 khoanThu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(khoanThu);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _khoanThuService.CreateKhoanThu(token, khoanThu);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager + ", " + Constant.Stocker)]
        [HttpPost("thanh-toan")]
        public IActionResult ThanhToan([FromBody] HoaDonDto1000 hoaDon)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _khoanThuService.ThanhToan(token, hoaDon);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager + ", " + Constant.Stocker)]
        [HttpPut("khoan-thu")]
        public IActionResult UpdateKhoanThu([FromBody] KhoanThuDto1002 khoanThu, int maKhoanThu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(khoanThu);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _khoanThuService.UpdateKhoanThu(token, maKhoanThu, khoanThu);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager + ", " + Constant.Stocker)]
        [HttpDelete("khoan-thu")]
        public IActionResult DeleteKhoanThu(int maKhoanThu, int version)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _khoanThuService.DeleteKhoanThu(maKhoanThu, token, version);
        }
    }
}