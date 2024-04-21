using CNPM.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CNPM.Core.Models.HoKhau
{
    public class HoKhauDto1003
    {
        [JsonProperty("maHoKhau")]
        public string? MaHoKhau { get; set; }
        [JsonProperty("soThanhVien")]
        public int SoThanhVien { get; set; }
        [JsonProperty("diaChiThuongTru")]
        public string? DiaChiThuongTru { get; set; }
        [JsonProperty("noiCap")]
        public string? NoiCap { get; set; }
        [JsonProperty("ngayCap")]
        public DateTime NgayCap { get; set; }
    }
}

