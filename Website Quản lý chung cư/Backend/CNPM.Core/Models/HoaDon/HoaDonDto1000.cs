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
namespace CNPM.Core.Models.HoaDon
{
    public class HoaDonDto1000
    {
        [Required]
        [JsonProperty("maKhoanThuTheoHo")]
        public int MaKhoanThuTheoHo { get; set; }
        [Required, StringLength(100)]
        [JsonProperty("tenHoaDon")]
        public string? TenHoaDon { get; set; }
        [Required]
        [JsonProperty("soTienDaNop")]
        public int SoTienDaNop { get; set; }
    }
}

