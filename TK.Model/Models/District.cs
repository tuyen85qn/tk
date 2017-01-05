using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TK.Model.Models
{
    [Table("Districts")]
    public class District
    {
        [Key]  
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { set; get; }
        [StringLength(100)]
        public string Name { set; get; }
        [StringLength(30)]
        public string Type { set; get; }
        public int ProvinceID { set; get; }
        [ForeignKey("ProvinceID")]
        public virtual Province Province { set; get; }
    }
}
