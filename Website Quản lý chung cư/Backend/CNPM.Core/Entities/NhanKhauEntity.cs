﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CNPM.Core.Entities
{
    [Table("nhankhau")]
    public class NhanKhauEntity : BaseEntity
    {
        [Key]
        [Required]
        public int MaNhanKhau { get; set; }
        [Required]
        [StringLength(100)]
        public string? HoTen { get; set; }
        [Required]
        [Column(TypeName = "varchar(12)")]
        public string? CanCuocCongDan { get; set; }
        [Required]
        public DateTime NgaySinh { get; set; }
        [Required]
        [StringLength(100)]
        public string? NoiSinh { get; set; }
        [Required]
        [StringLength(20)]
        public string? DanToc { get; set; }
        [Required]
        [StringLength(50)]
        public string? NgheNghiep { get; set; }
        [Required]
        [StringLength(30)]
        public string? QuanHe { get; set; }
        public string? MaHoKhau { get; set; }
        [ForeignKey("MaHoKhau")]
        public HoKhauEntity? HoKhau { get; set; }
        [StringLength(200)]
        public string? GhiChu { get; set; }
        [Required]
        public int TrangThai { get; set; }
    }
}