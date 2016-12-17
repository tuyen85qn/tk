using System;
using System.ComponentModel.DataAnnotations;

namespace TK.Model.Abstract
{
    public abstract class GroupCase : IGroupCase
    {
        [Required]
        [MaxLength(256)]
        public string Place { set; get; }

        public int? TheInjured { set; get; }
        public int? TheDead { set; get; }
        public decimal? PropertyDamage { set; get; }
        public int? SettleBodyID { set; get; }
        public int? ResolvedSituationID { set; get; }
        public DateTime? CreatedDate { set; get; }

        [MaxLength(256)]
        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        [MaxLength(256)]
        public string UpdatedBy { set; get; }

        [MaxLength(256)]
        public string MetaKeyword { set; get; }

        [MaxLength(256)]
        public string MetaDescription { set; get; }

        public bool Status { set; get; }
    }
}