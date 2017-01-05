using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdmin.Models
{
    public class WardViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Type { set; get; }
        public int DistrictID { set; get; }
        public int ProvinceID { set; get; }

    }
}