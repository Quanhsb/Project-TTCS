using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CNPM.Core.Entities
{
    [Table("hoadon")]
    public class HoaDonEntity : BaseEntity
    {
        [Key]
        [Required]
        public int MaHoaDon { get; set; }
        public int MaKhoanThuTheoHo { get; set; }
        [ForeignKey("MaKhoanThuTheoHo")]
        public KhoanThuTheoHoEntity KhoanThuTheoHo { get; set; }

        [Required, StringLength(100)]
        public string TenHoaDon { get; set; }

        [Required]
        public int SoTienDaNop { get; set; }

    }
}