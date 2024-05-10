﻿using CNPM.Core.Utils;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CNPM.Core.Models.HoKhau
{
    public class HoKhauDto1000
    {
        [MaxLength(10)]
        [JsonProperty("maHoKhau")]
        public string? MaHoKhau { get; set; }
        [MaxLength(200)]
        [Required]
        [JsonProperty("diaChiThuongTru")]
        public string? DiaChiThuongTru { get; set; }
        [MaxLength(200)]
        [Required]
        [JsonProperty("noiCap")]
        public string? NoiCap { get; set; }
        [Required]
        [JsonProperty("ngayCap")]
        public DateTime NgayCap { get; set; }
        [Required]
        [JsonProperty("danhSachNhanKhau")]
        public List<int>? DanhSachNhanKhau { get; set; }
    }
}