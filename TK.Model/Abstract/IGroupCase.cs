using System;
namespace TK.Model.Abstract
{
    public interface IGroupCase : IAuditable
    {
        string Place { set; get; }
        int? TheInjured { set; get; }
        int? TheDead { set; get; }
        decimal? PropertyDamage { set; get; }
        int? SettleBodyID { set; get; }
        int? ResolvedSituationID { set; get; }
        
    }
}