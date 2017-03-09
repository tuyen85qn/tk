using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdmin.Models
{
    public class SituationListViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string SituationCategoryName { set; get; }
        public String OccurenceDay { set; get; }       
        public string Place { set; get; }
        public int? TheInjured { set; get; }
        public int? TheDead { set; get; }
        public decimal? PropertyDamage { set; get; }
        public string PoliceOrganizationName { set; get; }
        public string ResolvedSituationName { set; get; }   
        public bool Status { set; get; }
    }
}