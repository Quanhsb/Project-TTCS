﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Core.Models;
using CNPM.Core.Models.NhanKhau;
using Microsoft.AspNetCore.Mvc;
namespace CNPM.Service.Interfaces
{
    public interface INhanKhauService
    {
        public IActionResult GetListNhanKhau(int index, int limit);
        public IActionResult GetListNhanKhauAlive(int index, int limit);
        public IActionResult GetListNhanKhauNotHaveHoKhau(int index, int limit);
        public IActionResult GetListNhanKhauInHoKhau(string maHoKhau);
        public IActionResult GetNhanKhau(int maNhanKhau);
        public IActionResult CreateNhanKhau(string token, NhanKhauDto1000 nhanKhau);
        public IActionResult UpdateNhanKhau(string token, int maNhanKhau, NhanKhauDto1002 nhanKhau);
        public IActionResult DeleteNhanKhau(int maNhanKhau, string token, int version);
    }
}