using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdmin.Models
{
    public class StatisticViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }


        public string Alias { set; get; }


        public int StatisticCategoryID { set; get; }     
        public DateTime FromDate { set; get; }       
        public DateTime ToDate { set; get; }

        public string Description { set; get; }

        public string Content { set; get; }
        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }
        public string Tags { set; get; }
        public int ProvinceID { set; get; }
        public int DistrictID { set; get; }
        public int WardID { set; get; }
        public int? TheInjured { set; get; }
        public int? TheDead { set; get; }
        public decimal? PropertyDamage { set; get; }
        public int? PoliceOrganizationID { set; get; }
        public int? ResolvedSituationID { set; get; }
        public DateTime? CreatedDate { set; get; }


        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }


        public string UpdatedBy { set; get; }


        public string MetaKeyword { set; get; }


        public string MetaDescription { set; get; }

        public bool Status { set; get; }
    }
}