using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TK.Model.Models{
   
    public class DailySheetStatistic
    {     
       
        public String FromDate { set; get; }

        public String ToDate { set; get; }

        public string Content { set; get; }

        public string PoliceOrganizationName { set; get; }
        public int TotalDailySheet { set; get; }
        public int TotalDay { set; get; }
       
    }
}