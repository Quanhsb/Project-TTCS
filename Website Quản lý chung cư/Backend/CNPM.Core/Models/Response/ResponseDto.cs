using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CNPM.Core.Models
{
    public class ResponseDto
    {
        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("data")]
        public object? Data { get; set; }
    }
}
