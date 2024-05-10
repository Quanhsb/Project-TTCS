﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CNPM.Core.Entities
{
    [Table("login_info")]
    public class LoginInfoEntity : BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string? UserName { get; set; }
        [ForeignKey("UserName")]
        public UserEntity? User { get; set; }
        [Required]
        [Column(TypeName = "varchar(400)")]
        public string? AccessToken { get; set; }
    }
}