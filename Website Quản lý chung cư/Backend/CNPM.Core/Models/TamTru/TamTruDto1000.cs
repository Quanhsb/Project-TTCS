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
namespace CNPM.Core.Models.TamTru
{
    public class TamTruDto1000
    {
        [Required]
        [MaxLength(100)]
        [JsonProperty("hoTen")]
        public string? HoTen { get; set; }
        [Required]
        [MaxLength(100)]
        [JsonProperty("diaChiThuongTru")]
        public string? DiaChiThuongTru { get; set; }
        [Required]
        [MaxLength(100)]
        [JsonProperty("diaChiTamTru")]
        public string? DiaChiTamTru { get; set; }
        [Required]
        [MaxLength(20)]
        [JsonProperty("canCuocCongDan")]
        public string? CanCuocCongDan { get; set; }
    }
}