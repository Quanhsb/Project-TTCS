using CNPM.Core.Entities;
using CNPM.Core.Models.NhanKhau;
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
namespace CNPM.Core.Models.TamTru
{
    public class TamTruDto1001
    {
        [JsonProperty("maTamTru")]
        public string? MaTamTru { get; set; }
        [JsonProperty("hoTen")]
        public string? HoTen { get; set; }
        [JsonProperty("diaChiThuongTru")]
        public string? DiaChiThuongTru { get; set; }
        [JsonProperty("diaChiTamTru")]
        public string? DiaChiTamTru { get; set; }
        [JsonProperty("canCuocCongDan")]
        public string? CanCuocCongDan { get; set; }
        [JsonProperty("createTime")]
        public DateTime CreateTime { get; set; }
        [JsonProperty("updateTime")]
        public DateTime UpdateTime { get; set; }
        [JsonProperty("userCreate")]
        public string? UserCreate { get; set; }
        [JsonProperty("userUpdate")]
        public string? UserUpdate { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
    }
}

