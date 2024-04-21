using CNPM.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Core.Models.Xe
{
    public class XeDto1003 // body get list
    {
        [JsonProperty("maXe")]
        public int MaXe { get; set; }

        [JsonProperty("tenXe")]
        public string TenXe { get; set; }

        [JsonProperty("bienKiemSoat")]
        public string BienKhiemSoat { get; set; }

        [JsonProperty("maLoaiXe")]
        public string MaLoaiXe { get; set; }

        [JsonProperty("maHoKhau")]
        public string MaHoKhau { get; set; }

        [JsonProperty("moTa")]
        public string MoTa { get; set; }
    }
}