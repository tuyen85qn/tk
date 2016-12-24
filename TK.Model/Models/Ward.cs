using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TK.Model.Models
{
    [Table("Wards")]
    public class Ward
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        [StringLength(100)]
        public string Name { set; get; }
        [StringLength(30)]
        public string Type { set; get; }
        public int ProvinceID { set; get; }        
        public int DistrictID { set; get; }       
        [ForeignKey("DistrictID")]
        public virtual District District { set; get; }
    }
}
