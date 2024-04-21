using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CNPM.Core.Models
{
    public class UserDto1004
    {
        [Required]
        [JsonProperty("userName")]
        [MaxLength(40)]
        public string? UserName { get; set; }
        [Required]
        [MinLength(5)]
        [JsonProperty("password")]
        public string? Password { get; set; }
    }
}
