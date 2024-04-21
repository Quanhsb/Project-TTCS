using CNPM.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CNPM.Core.Models.TamVang
{
    public class TamVangDto1002
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
        [JsonProperty("version")]
        public int Version { get; set; }
    }
}

