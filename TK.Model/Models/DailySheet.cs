using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.Model.Abstract;

namespace TK.Model.Models
{
    [Table("DailySheets")]
    public class DailySheet: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        public DateTime DayReport { set; get; }
        public int PoliceOrganizationID { set; get; }
        [StringLength(20)]
        public string DirectCommand { set; get; }
        [StringLength(20)]
        public string OnDuty { set; get; }
        public string FileDailySheet { set; get; }
        public int? TypeReportID { set; get; }
        [MaxLength(500)]
        public string Description { set; get; }       
        
        [ForeignKey("PoliceOrganizationID")]
        public virtual PoliceOrganization PoliceOrganization { set; get; }
        [ForeignKey("TypeReportID")]
        public virtual TypeReport TypeReport { set; get; }
        
    }
}
