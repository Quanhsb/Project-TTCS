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
    public class TamVangDto1003
    {
        [JsonProperty("maTamVang")]
        public int MaTamVang { get; set; }
        [JsonProperty("maNhanKhau")]
        public int MaNhanKhau { get; set; }
        [JsonProperty("hoTen")]
        public string? HoTen { get; set; }
        [JsonProperty("canCuocCongDan")]
        public string? CanCuocCongDan { get; set; }
        [JsonProperty("thoiHan")]
        public DateTime ThoiHan { get; set; }
        [MaxLength(200)]
        public string? LyDo { get; set; }
    }
}

