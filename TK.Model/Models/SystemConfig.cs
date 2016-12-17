using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TK.Model.Models
{
    [Table("SystemConfigs")]
    public class SystemConfig
    {
        [Key]
        public int ID { set; get; }
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public String Code { set; get; }
        [MaxLength(50)]
        public String ValueString { set; get; }
        public int? ValueInt { set; get; }
    }
}
