using System;
namespace TK.Model.Abstract
{
    public interface IGroupCase : IAuditable
    {
        
        int ProvinceID { set; get; }
        int? DistrictID { set; get; }
        int? WardID { set; get; }
        int? TheInjured { set; get; }
        int? TheDead { set; get; }
        decimal? PropertyDamage { set; get; }
        int? PoliceOrganizationID { set; get; }
        int? ResolvedSituationID { set; get; }
        
    }
}