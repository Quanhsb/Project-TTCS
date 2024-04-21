using CNPM.Core.Entities;
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
    public class KhoanThuDto1004
    {
        [JsonProperty("maKhoanThuTheoHo")]
        public int MaKhoanThuTheoHo { get; set; }
        [JsonProperty("maHoKhau")]
        public string? MaHoKhau { get; set; }
        [JsonProperty("soTien")]
        public int SoTien { get; set; }
        [JsonProperty("soTienDaNop")]
        public int SoTienDaNop { get; set; }
    }
}

