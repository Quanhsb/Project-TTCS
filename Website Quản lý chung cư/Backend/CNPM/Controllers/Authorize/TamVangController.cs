﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CNPM.Service.Interfaces;
using CNPM.Core.Models;
using CNPM.Core.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CNPM.Service.Implementations;
using CNPM.Core.Models.TamVang;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace CNPM.Controllers.Authorize
{
    [Authorize]
    [ApiController]
    [Route(Constant.API_BASE)]
    public class TamVangController : ControllerBase
    {
        private readonly ITamVangService _tamVangService;
        public TamVangController(ITamVangService tamVangService)
        {
            _tamVangService = tamVangService;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpGet("tam-vang/danh-sach-tam-vang")]
        public IActionResult GetListTamVang(int index, int limit)
        {
            return _tamVangService.GetListTamVang(index, limit);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpGet("tam-vang")]
        public IActionResult GetTamVang(int maTamVang)
        {
            return _tamVangService.GetTamVang(maTamVang);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpPost("tam-vang")]
        public IActionResult CreateTamVang([FromBody] TamVangDto1000 tamVang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(tamVang);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _tamVangService.CreateTamVang(token, tamVang);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpPut("tam-vang")]
        public IActionResult UpdateTamVang([FromBody] TamVangDto1002 tamVang, int maTamVang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(tamVang);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _tamVangService.UpdateTamVang(token, maTamVang, tamVang);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpDelete("tam-vang")]
        public IActionResult DeleteTamVang(int maTamVang, int version)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _tamVangService.DeleteTamVang(maTamVang, token, version);
        }
    }
}