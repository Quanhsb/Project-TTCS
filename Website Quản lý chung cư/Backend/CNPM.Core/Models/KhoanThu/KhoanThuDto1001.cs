﻿using CNPM.Core.Entities;
using CNPM.Core.Utils;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CNPM.Core.Models.KhoanThu
{
    public class KhoanThuDto1001
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
        [JsonProperty("ghiChu")]
        public string? GhiChu { get; set; }
        [JsonProperty("createTime")]
        public DateTime CreateTime { get; set; }
        [JsonProperty("updateTime")]
        public DateTime UpdateTime { get; set; }
        [JsonProperty("userCreate")]
        public string? UserCreate { get; set; }
        [JsonProperty("userUpdate")]
        public string? UserUpdate { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
    }
}