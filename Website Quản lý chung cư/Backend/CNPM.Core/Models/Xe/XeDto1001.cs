﻿﻿using CNPM.Core.Entities;
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
namespace CNPM.Core.Models.Xe
{
    public class XeDto1001
    {
        [JsonProperty("maXe")]
        public int MaXe { get; set; }
        [JsonProperty("tenXe")]
        public string? TenXe { get; set; }
        [JsonProperty("bienKiemSoat")]
        public string? BienKiemSoat { get; set; }
        [JsonProperty("maLoaiXe")]
        public string? MaLoaiXe { get; set; }
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