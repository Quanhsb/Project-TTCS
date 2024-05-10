﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CNPM.Core.Entities
{
    [Table("khoanthutheoho")]
    public class KhoanThuTheoHoEntity : BaseEntity
    {
        [Key]
        [Required]
        public int MaKhoanThuTheoHo { get; set; }
        public string? MaHoKhau { get; set; }
        [ForeignKey("MaHoKhau")]
        public HoKhauEntity? HoKhau { get; set; }
        public int MaKhoanThu { get; set; }
        [ForeignKey("MaKhoanThu")]
        public KhoanThuEntity? KhoanThu { get; set; }
        public int? SoTien { get; set; }
    }
}