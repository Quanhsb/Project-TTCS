﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace CNPM.Core.Models
{
    public class UserDto1005
    {
        [Required]
        [JsonProperty("userName")]
        [MaxLength(40)]
        public string? UserName { get; set; }
        [Required]
        [MinLength(5)]
        [JsonProperty("password")]
        public string? Password { get; set; }
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
        [JsonProperty("userCreate")]
        public string? UserCreate { get; set; }
    }
}