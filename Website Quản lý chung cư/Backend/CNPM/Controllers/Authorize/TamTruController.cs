﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CNPM.Service.Interfaces;
using CNPM.Core.Models;
using CNPM.Core.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CNPM.Service.Implementations;
using CNPM.Core.Models.TamTru;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace CNPM.Controllers.Authorize
{
    [Authorize]
    [ApiController]
    [Route(Constant.API_BASE)]
    public class TamTruController : ControllerBase
    {
        private readonly ITamTruService _tamTruService;
        public TamTruController(ITamTruService tamTruService)
        {
            _tamTruService = tamTruService;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpGet("tam-tru/danh-sach-tam-tru")]
        public IActionResult GetListTamTru(int index, int limit)
        {
            return _tamTruService.GetListTamTru(index, limit);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpGet("tam-tru")]
        public IActionResult GetTamTru(int maTamTru)
        {
            return _tamTruService.GetTamTru(maTamTru);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpPost("tam-tru")]
        public IActionResult CreateTamTru([FromBody] TamTruDto1000 tamTru)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(tamTru);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _tamTruService.CreateTamTru(token, tamTru);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpPut("tam-tru")]
        public IActionResult UpdateTamTru([FromBody] TamTruDto1002 tamTru, int maTamTru)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(tamTru);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _tamTruService.UpdateTamTru(token, maTamTru, tamTru);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Constant.Administrator + ", " + Constant.Manager)]
        [HttpDelete("tam-tru")]
        public IActionResult DeleteTamTru(int maTamTru, int version)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return _tamTruService.DeleteTamTru(maTamTru, token, version);
        }
    }
}