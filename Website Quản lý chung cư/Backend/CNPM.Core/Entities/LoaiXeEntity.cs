using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CNPM.Core.Entities
{
    [Table("loaixe")]
    public class LoaiXeEntity : BaseEntity
    {
        [Key]
        [Required]
        public string MaLoaiXe { get; set; }

        [Required]
        [StringLength(100)]
        public string LoaiXe { get; set; }
    }
}