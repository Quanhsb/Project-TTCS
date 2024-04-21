using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CNPM.Core.Entities
{
    [Table("lichsu")]
    public class LichSuEntity : BaseEntity
    {
        [Key]
        [Required]
        public int MaLichSu { get; set; }

        public string MaHoKhau { get; set; }

        [ForeignKey("MaHoKhau")]
        public HoKhauEntity HoKhau { get; set; }

        [Required, StringLength(500)]
        public string NoiDung { get; set; }
    }
}