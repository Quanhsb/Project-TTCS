﻿using CNPM.Core.Entities;
using CNPM.Core.Models.Common;
using CNPM.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Core.Models.KhoanThu
{
    public class PhiGuiXe // khoan thu quan ly
    {
        [JsonProperty("xeMay")]
        public SoTienAndDonVi XeMay { get; set; }
        [JsonProperty("xeOto")]
        public SoTienAndDonVi XeOto { get; set; }
    }
}