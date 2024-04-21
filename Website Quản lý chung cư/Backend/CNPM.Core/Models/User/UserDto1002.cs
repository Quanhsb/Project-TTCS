using Newtonsoft.Json;
namespace CNPM.Core.Models
{
    public class UserDto1002
    {
        [JsonProperty("userName")]
        public string? UserName { get; set; }
        [JsonProperty("firstName")]
        public string? FirstName { get; set; }
        [JsonProperty("lastName")]
        public string? LastName { get; set; }
        [JsonProperty("roleId")]
        public int RoleId { get; set; }
        [JsonProperty("token")]
        public string? Token { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
    }
}
