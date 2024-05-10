using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CNPM.Core.Entities
{
    [Table("canho")]
    public class CanHoEntity : BaseEntity
    {
        [Key]
        [Required]
        public int MaCanHo { get; set; }
        [Required]
        [StringLength(100)]
        public string? TenCanHo { get; set; }
        public string? Tang { get; set; }
        [Required]
        [Range(0, 1000)]
        public double DienTich { get; set; }
        public string? MaHoKhau { get; set; }
        [ForeignKey("MaHoKhau")]
        public HoKhauEntity? HoKhau { get; set; }
        public string? MoTa { get; set; }
    }
}