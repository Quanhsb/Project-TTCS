﻿using CNPM.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CNPM.Core.Models.CanHo
{
    public class CanHoDto1003
    {
        [JsonProperty("maCanHo")]
        public int MaCanHo { get; set; }
        [JsonProperty("tenCanHo")]
        public string? TenCanHo { get; set; }
        [JsonProperty("tang")]
        public string? Tang { get; set; }
        [JsonProperty("maHoKhau")]
        public string? MaHoKhau { get; set; }
        [JsonProperty("dienTich")]
        public double DienTich { get; set; }
        [JsonProperty("moTa")]
        public string? MoTa { get; set; }
    }
}