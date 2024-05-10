﻿using CNPM.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CNPM.Core.Models.KhoanThu
{
    public class KhoanThuDto1003
    {
        [JsonProperty("maKhoanThu")]
        public int MaKhoanThu { get; set; }
        [JsonProperty("tenKhoanThu")]
        public string? TenKhoanThu { get; set; }
        [JsonProperty("thoiGianBatDau")]
        public DateTime ThoiGianBatDau { get; set; }
        [JsonProperty("thoiGianKetThuc")]
        public DateTime ThoiGianKetThuc { get; set; }
        [JsonProperty("loaiKhoanThu")]
        public int LoaiKhoanThu { get; set; }
        [JsonProperty("chiTiet")]
        public string? ChiTiet { get; set; }
    }
}