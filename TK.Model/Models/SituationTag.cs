using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TK.Model.Models
{
    [Table("SituationTags")]
    public class SituationTag
    {
        [Key]
        [Column(Order = 1)]
        public int ProductID { set; get; }
        [Key]
        [Column(TypeName = "varchar", Order = 2)]
        [MaxLength(50)]
        public String TagID { set; get; }
        [ForeignKey("SituationID")]
        public virtual Situation Situation { set; get; }
        [ForeignKey("TagID")]
        public virtual Tag Tag { set; get; }
    }
}
