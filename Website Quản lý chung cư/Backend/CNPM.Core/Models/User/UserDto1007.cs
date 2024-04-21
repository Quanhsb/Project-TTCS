using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace CNPM.Core.Models
{
    public class UserDto1007
    {
        [Required]
        [JsonProperty("userName")]
        [MaxLength(40)]
        public string? UserName { get; set; }
        [Required]
        [JsonProperty("userUpdate")]
        public string? UserUpdate { get; set; }
        [Required]
        [JsonProperty("version")]
        public int Version { get; set; }
    }
}
