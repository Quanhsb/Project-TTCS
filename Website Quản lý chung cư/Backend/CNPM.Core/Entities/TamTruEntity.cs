using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CNPM.Core.Entities
{
    [Table("tamtru")]
    public class TamTruEntity : BaseEntity
    {
        [Key]
        [Required]
        public int MaTamTru { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Required]
        [StringLength(100)]
        public string DiaChiThuongTru { get; set; }

        [Required]
        [StringLength(100)]
        public string DiaChiTamTru { get; set; }

        [Required]
        [StringLength(20)]
        public string CanCuocCongDan { get; set; }
    }
}