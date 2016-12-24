using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TK.Model.Models
{
    [Table("StatisticTags")]
    public class StatisticTag
    {
        [Key]
        [Column(Order = 1)]
        public int StatisticID { set; get; }
        [Key]
        [Column(TypeName = "varchar", Order = 2)]
        [MaxLength(50)]
        public String TagID { set; get; }
        [ForeignKey("StatisticID")]
        public virtual Statistic Statistic { set; get; }
        [ForeignKey("TagID")]
        public virtual Tag Tag { set; get; }
    }
}
