﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CNPM.Service.Interfaces;
using CNPM.Core.Models;
using CNPM.Core.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CNPM.Service.Implementations;
using CNPM.Core.Models.CanHo;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace CNPM.Controllers.Authorize
{
    [Authorize]
    [ApiController]
    [Route(Constant.API_BASE)]
    public class CanHoController : ControllerBase
    {
        private readonly ICanHoService _canHoService;
        public CanHoController(ICanHoService canHoService)
        {
            _canHoService = canHoService;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpGet("can-ho/danh-sach-can-ho")]
        public IActionResult GetListcanHo(int index, int limit)
        {
            return _canHoService.GetListCanHo(index, limit);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpGet("can-ho")]
        public IActionResult GetcanHo(int maCanHo)
        {
            return _canHoService.GetCanHo(maCanHo);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpPost("can-ho")]
        public IActionResult CreatecanHo([FromBody] CanHoDto1000 canHo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(canHo);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _canHoService.CreateCanHo(token, canHo);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpPut("can-ho")]
        public IActionResult UpdatecanHo([FromBody] CanHoDto1002 newcanHo, int maCanHo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(newcanHo);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _canHoService.UpdateCanHo(token, maCanHo, newcanHo);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpDelete("can-ho")]
        public IActionResult DeletecanHo(int maCanHo, int version)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _canHoService.DeleteCanHo(maCanHo, token, version);
        }
    }
}