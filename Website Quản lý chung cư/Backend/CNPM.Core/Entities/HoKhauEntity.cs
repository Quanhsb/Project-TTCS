﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CNPM.Core.Entities
{
    [Table("hokhau")]
    public class HoKhauEntity : BaseEntity
    {
        [Key]
        [Required]
        [Column(TypeName = "varchar(10)")]
        public string? MaHoKhau { get; set; }
        [StringLength(200)]
        [Required]
        public string? DiaChiThuongTru { get; set; }
        [StringLength(200)]
        [Required]
        public string? NoiCap { get; set; }
        [Required]
        public DateTime NgayCap { get; set; }
    }
}