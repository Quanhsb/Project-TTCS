﻿using CNPM.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CNPM.Core.Models.NhanKhau
{
    public class NhanKhauDto1000
    {
        [Required]
        [StringLength(100)]
        [JsonProperty("hoTen")]
        public string? HoTen { get; set; }
        [Required]
        [MaxLength(12)]
        [JsonProperty("canCuocCongDan")]
        public string? CanCuocCongDan { get; set; }
        [Required]
        [JsonProperty("ngaySinh")]
        public DateTime NgaySinh { get; set; }
        [Required]
        [StringLength(100)]
        [JsonProperty("noiSinh")]
        public string? NoiSinh { get; set; }
        [Required]
        [StringLength(20)]
        [JsonProperty("danToc")]
        public string? DanToc { get; set; }
        [Required]
        [StringLength(50)]
        [JsonProperty("ngheNghiep")]
        public string? NgheNghiep { get; set; }
        [Required]
        [CustomValidation(typeof(TrangThaiAttribute), "IsTrangThai")]
        [JsonProperty("trangThai")]
        public int TrangThai { get; set; }
        [Required]
        [StringLength(30)]
        [JsonProperty("quanHe")]
        public string? QuanHe { get; set; }
        [MaxLength(200)]
        [JsonProperty("ghiChu")]
        public string? GhiChu { get; set; }
    }
}