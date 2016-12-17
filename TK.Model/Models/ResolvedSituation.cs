using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TK.Model.Models
{
    [Table("ResolvedSituations")]
    public class ResolvedSituation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }    
        public virtual IEnumerable<Situation> Situations { set; get; }
        public virtual IEnumerable<Statistic> Statistics { set; get; }

    }
}