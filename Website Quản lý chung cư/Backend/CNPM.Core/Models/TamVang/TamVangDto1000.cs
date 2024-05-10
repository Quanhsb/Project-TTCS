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
namespace CNPM.Core.Models.TamVang
{
    public class TamVangDto1000
    {
        [Required]
        [JsonProperty("maNhanKhau")]
        public int MaNhanKhau { get; set; }
        [Required]
        [JsonProperty("thoiHan")]
        public DateTime ThoiHan { get; set; }
        [Required]
        [MaxLength(200)]
        [JsonProperty("lyDo")]
        public string? LyDo { get; set; }
    }
}