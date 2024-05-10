﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CNPM.Core.Entities
{
    public class BaseEntity
    {
        [Required]
        public DateTime CreateTime { get; set; }
        [Required]
        public DateTime UpdateTime { get; set; }
        [Required]
        public string? UserCreate { get; set; }
        [Required]
        public string? UserUpdate { get; set; }
        [Required]
        public int Version { get; set; }
        [Required]
        public int Delete { get; set; }
    }
}