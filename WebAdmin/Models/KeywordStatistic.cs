using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdmin.Models
{
    public class KeywordStatistic
    {
        public String FromDate { set; get; }
        public String ToDate { set; get; }
        public int ProvinceID { set; get; }
        public int? DistrictID { set; get; }
        public int? ResolvedSituationID { set; get; }
    }
}