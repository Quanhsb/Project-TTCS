﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CNPM.Core.Entities
{
    [Table("tamvang")]
    public class TamVangEntity : BaseEntity
    {
        [Key]
        [Required]
        public int MaTamVang { get; set; }
        [Required]
        public int MaNhanKhau { get; set; }
        [ForeignKey("MaNhanKhau")]
        public NhanKhauEntity? NhanKhau { get; set; }
        [Required]
        public DateTime ThoiHan { get; set; }
        [Required, StringLength(200)]
        public string? LyDo { get; set; }
    }
}