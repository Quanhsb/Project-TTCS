﻿﻿using CNPM.Core.Entities;
using CNPM.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CNPM.Core.Models.Common
{
    public class SoTienAndDonVi
    {
        [JsonProperty("soTien")]
        public int SoTien;
        [JsonProperty("donVi")]
        public string? DonVi;
    }
}