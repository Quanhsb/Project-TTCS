﻿﻿using CNPM.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CNPM.Core.Models.Xe
{
    public class XeDto1002
    {
        [Required]
        [JsonProperty("maXe")]
        public int MaXe { get; set; }
        [MaxLength(100)]
        [Required]
        [JsonProperty("tenXe")]
        public string? TenXe { get; set; }
        [MaxLength(200)]
        [Required]
        [JsonProperty("bienKiemSoat")]
        public string? BienKiemSoat { get; set; }
        [Required]
        [JsonProperty("maLoaiXe")]
        public string? MaLoaiXe { get; set; }
        [JsonProperty("maHoKhau")]
        public string? MaHoKhau { get; set; }
        [JsonProperty("moTa")]
        public string? MoTa { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
    }
}