using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace CNPM.Core.Models
{
    public class UserDto1006
    {
        [Required]
        [JsonProperty("userName")]
        [MaxLength(40)]
        public string? UserName { get; set; }
        [Required]
        [JsonProperty("firstName")]
        [MaxLength(100)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        [JsonProperty("lastName")]
        public string? LastName { get; set; }
        [Required]
        [JsonProperty("email")]
        [MaxLength(100)]
        public string? Email { get; set; }
        [Required]
        [JsonProperty("roleId")]
        public int RoleId { get; set; }
        [Required]
        [JsonProperty("userUpdate")]
        public string? UserUpdate { get; set; }
        [Required]
        [JsonProperty("version")]
        public int Version { get; set; }
    }
}
