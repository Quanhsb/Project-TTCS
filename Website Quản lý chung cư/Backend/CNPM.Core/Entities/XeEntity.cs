﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CNPM.Core.Entities
{
    [Table("xe")]
    public class XeEntity : BaseEntity
    {
        [Key]
        [Required]
        public int MaXe { get; set; }
        [Required]
        [StringLength(100)]
        public string? TenXe { get; set; }
        [Required]
        [StringLength(100)]
        public string? BienKiemSoat { get; set; }
        [Required]
        public string? MaLoaiXe { get; set; }
        [ForeignKey("MaLoaiXe")]
        public LoaiXeEntity? LoaiXe { get; set; }
        public string? MaHoKhau { get; set; }
        [ForeignKey("MaHoKhau")]
        public HoKhauEntity? HoKhau { get; set; }
        public string? MoTa { get; set; }
    }
}