﻿using CNPM.Core.Entities;
using CNPM.Core.Models.NhanKhau;
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
using CNPM.Core.Models.CanHo;
using CNPM.Core.Models.Xe;
namespace CNPM.Core.Models.HoKhau
{
    public class HoKhauDto1001
    {
        [JsonProperty("maHoKhau")]
        public string? MaHoKhau { get; set; }
        [JsonProperty("soThanhVien")]
        public int SoThanhVien { get; set; }
        [JsonProperty("diaChiThuongTru")]
        public string? DiaChiThuongTru { get; set; }
        [JsonProperty("noiCap")]
        public string? NoiCap { get; set; }
        [JsonProperty("ngayCap")]
        public DateTime NgayCap { get; set; }
        [JsonProperty("danhSachNhanKhau")]
        public List<NhanKhauDto1001>? DanhSachNhanKhau { get; set; }
        [JsonProperty("maCanHo")]
        public int MaCanHo { get; set; }
        [JsonProperty("danhSachXe")]
        public List<XeDto1001>? DanhSachXe { get; set; }
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