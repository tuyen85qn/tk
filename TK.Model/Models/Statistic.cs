using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TK.Model.Abstract;

namespace TK.Model.Models
{
    [Table("Statistics")]
    public class Statistic : GroupCase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        [Column(TypeName = "varchar")]
        public string Alias { set; get; }

        [Required]
        public int StatisticCategoryID { set; get; }
        [Required]
        public DateTime FromDate { set; get; }
        [Required]
        public DateTime ToDate { set; get; }
                    
        public string Content { set; get; }
        public int TotalSituationCount { set; get; }        
        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }
        public string Tags { set; get; }

        [ForeignKey("StatisticCategoryID")]
        public virtual StatisticCategory StatisticCategory { set; get; }

        [ForeignKey("PoliceOrganizationID")]
        public virtual PoliceOrganization PoliceOrganization { set; get; }

        [ForeignKey("ResolvedSituationID")]
        public virtual ResolvedSituation ResolvedSituation { set; get; }
        [ForeignKey("ProvinceID")]
        public virtual Province Province { set; get; }
        [ForeignKey("DistrictID")]
        public virtual District District { set; get; }
        [ForeignKey("WardID")]
        public virtual Ward Ward { set; get; }
    }
}