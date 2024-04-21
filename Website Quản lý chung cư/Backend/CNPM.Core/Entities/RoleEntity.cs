using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CNPM.Core.Entities
{
    [Table("role")]
    public class RoleEntity
    {
        //public int UserId { get; set; }
        public RoleEntity(string roleName)
        {
            RoleName = roleName;
        }

        [Key]
        [Required]
        public int RoleId { get; set; }

        [StringLength(100)]
        [Required]
        public string RoleName { get; set; }


    }
}