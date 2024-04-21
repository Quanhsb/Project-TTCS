using Newtonsoft.Json;
namespace CNPM.Core.Models
{
    public class UserDto1000
    {
        [JsonProperty("userName")]
        public string? UserName { get; set; }
        [JsonProperty("oldPassword")]
        public string? OldPassword { get; set; }
        [JsonProperty("newPassword")]
        public string? NewPassword { get; set; }
    }
}
