﻿﻿using CNPM.Core.Utils;
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
    public class XeDto1000 // body create
    {
        [MaxLength(100)]
        [Required]
        [JsonProperty("tenXe")]
        public string TenXe { get; set; }
        [MaxLength(200)]
        [Required]
        [JsonProperty("bienKiemSoat")]
        public string BienKiemSoat { get; set; }

        [Required]
        [JsonProperty("maLoaiXe")]
        public string MaLoaiXe { get; set; }
        [Required]
        [JsonProperty("maHoKhau")]
        public string MaHoKhau { get; set; }
        [JsonProperty("moTa")]
        public string? MoTa { get; set; }
    }
}