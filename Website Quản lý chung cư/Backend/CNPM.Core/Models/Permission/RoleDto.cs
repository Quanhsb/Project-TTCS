﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CNPM.Core.Models
{
    public class RoleDto
    {
        [JsonProperty("roleId")]
        public int RoleId { get; set; }
        [JsonProperty("roleName")]
        public string? RoleName { get; set; }
    }
}