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
namespace CNPM.Core.Models.NhanKhau
{
    public class NhanKhauDto1001
    {
        [JsonProperty("maNhanKhau")]
        public int MaNhanKhau { get; set; }
        [JsonProperty("hoTen")]
        public string? HoTen { get; set; }
        [JsonProperty("canCuocCongDan")]
        public string? CanCuocCongDan { get; set; }
        [JsonProperty("ngaySinh")]
        public DateTime NgaySinh { get; set; }
        [JsonProperty("noiSinh")]
        public string? NoiSinh { get; set; }
        [JsonProperty("danToc")]
        public string? DanToc { get; set; }
        [JsonProperty("ngheNghiep")]
        public string? NgheNghiep { get; set; }
        [JsonProperty("quanHe")]
        public string? QuanHe { get; set; }
        [JsonProperty("maHoKhau")]
        public string? MaHoKhau { get; set; }
        [JsonProperty("ghiChu")]
        public string? GhiChu { get; set; }
        [JsonProperty("trangThai")]
        public int TrangThai { get; set; }
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