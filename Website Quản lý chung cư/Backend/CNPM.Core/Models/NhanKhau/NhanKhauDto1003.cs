using CNPM.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CNPM.Core.Models.NhanKhau
{
    public class NhanKhauDto1003
    {
        [JsonProperty("maNhanKhau")]
        public int MaNhanKhau { get; set; }
        [JsonProperty("hoTen")]
        public string? HoTen { get; set; }
        [JsonProperty("canCuocCongDan")]
        public string? CanCuocCongDan { get; set; }
        [JsonProperty("ngaySinh")]
        public DateTime NgaySinh { get; set; }
        [JsonProperty("noiSinh")]
        public string? NoiSinh { get; set; }
        [JsonProperty("danToc")]
        public string? DanToc { get; set; }
        [JsonProperty("trangThai")]
        public int TrangThai { get; set; }
    }
}

