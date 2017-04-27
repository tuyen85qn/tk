using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAdmin.Models
{
    public class DailySheetShowViewModel
    {
        public int ID { set; get; }
        public String DayReport { set; get; }
        public String PoliceOrganizationName { set; get; }
       
        public string DirectCommand { set; get; }
    
        public string OnDuty { set; get; }

        public String TypeReportName { set; get; }
      
        public string Description { set; get; }
        public string FileDailySheet { set; get; }
        public DateTime? CreatedDate { set; get; }

     
        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

     
        public string UpdatedBy { set; get; }

      
        public string MetaKeyword { set; get; }

      
        public string MetaDescription { set; get; }

        public bool Status { set; get; }
    }
}