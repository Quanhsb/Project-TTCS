﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Core.Entities;
using CNPM.Core.Models;
using CNPM.Core.Models.HoaDon;
using CNPM.Core.Models.KhoanThu;
using Microsoft.AspNetCore.Mvc;
namespace CNPM.Service.Interfaces
{
    public interface IKhoanThuService
    {
        public IActionResult GetListKhoanThu(int index, int limit);
        public IActionResult GetKhoanThu(int maKhoanThu);
        public IActionResult CreateKhoanThu(string token, KhoanThuDto1000 khoanThu);
        public IActionResult CreateKhoanThuTheoHo(string token, int maKhoanThu);
        public IActionResult GetKhoanThuTheoHo(int maKhoanThu);
        public IActionResult GetCacKhoanThuDaNopCuaHo(string maHoKhau);
        public IActionResult UpdateKhoanThu(string token, int maKhoanThu, KhoanThuDto1002 khoanThu);
        public IActionResult DeleteKhoanThu(int maKhoanThu, string token, int version);
        public IActionResult ThanhToan(string token, HoaDonDto1000 hoaDon);
    }
}