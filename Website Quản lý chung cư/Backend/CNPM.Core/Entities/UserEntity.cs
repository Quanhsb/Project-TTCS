﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CNPM.Core.Entities
{
    [Table("user")]
    public class UserEntity : BaseEntity
    {
        [Key]
        [Required]
        [Column(TypeName = "varchar(40)")]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [StringLength(100)]
        [Required]
        public string? FirstName { get; set; }
        [StringLength(100)]
        [Required]
        public string? LastName { get; set; }
        public int RoleId { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? Email { get; set; }
        [ForeignKey("RoleId")]
        public RoleEntity? Role { get; set; }
    }
}