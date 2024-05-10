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
    public class KhoanThuDto1002
    {
        [Required]
        [StringLength(100)]
        [JsonProperty("tenKhoanThu")]
        public string? TenKhoanThu { get; set; }
        [Required]
        [JsonProperty("thoiGianBatDau")]
        public DateTime ThoiGianBatDau { get; set; }
        [Required]
        [JsonProperty("thoiGianKetThuc")]
        public DateTime ThoiGianKetThuc { get; set; }
        [Required]
        [JsonProperty("chiTiet")]
        public string? ChiTiet { get; set; }
        [MaxLength(200)]
        [JsonProperty("ghiChu")]
        public string? GhiChu { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
    }
}