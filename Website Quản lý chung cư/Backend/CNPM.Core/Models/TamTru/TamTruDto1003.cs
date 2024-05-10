﻿using CNPM.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CNPM.Core.Models.TamTru
{
    public class TamTruDto1003
    {
        [JsonProperty("maTamTru")]
        public string? MaTamTru { get; set; }
        [JsonProperty("hoTen")]
        public string? HoTen { get; set; }
        [JsonProperty("diaChiThuongTru")]
        public string? DiaChiThuongTru { get; set; }
        [JsonProperty("diaChiTamTru")]
        public string? DiaChiTamTru { get; set; }
        [JsonProperty("canCuocCongDan")]
        public string? CanCuocCongDan { get; set; }
    }
}