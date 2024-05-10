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
namespace CNPM.Core.Models.CanHo
{
    public class CanHoDto1001
    {
        [JsonProperty("maCanHo")]
        public int MaCanHo { get; set; }
        [JsonProperty("tenCanHo")]
        public string? TenCanHo { get; set; }
        [JsonProperty("tang")]
        public string? Tang { get; set; }
        [JsonProperty("dienTich")]
        public double DienTich { get; set; }
        [JsonProperty("maHoKhau")]
        public string? MaHoKhau { get; set; }
        [JsonProperty("moTa")]
        public string? MoTa { get; set; }
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