using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdmin.Models
{
    public class DailySheetViewModel
    {
        public int ID { set; get; }
        public String DayReport { set; get; }
        public int PoliceOrganizationID { set; get; }

        public string DirectCommand { set; get; }

        public string OnDuty { set; get; }

        public int TypeReportID { set; get; }

        public string Description { set; get; }
        public DateTime? CreatedDate { set; get; }


        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }


        public string UpdatedBy { set; get; }


        public string MetaKeyword { set; get; }


        public string MetaDescription { set; get; }

        public bool Status { set; get; }
    }
}