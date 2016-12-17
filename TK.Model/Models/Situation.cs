﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TK.Model.Abstract;

namespace TK.Model.Models
{
    [Table("Situations")]
    public class Situation : GroupCase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        [Column(TypeName = "varchar")]
        public string Alias { set; get; }

        [Required]
        public int SituationCategoryID { set; get; }
              
        [MaxLength(256)]
        public string Image { set; get; }

        [Column(TypeName = "xml")]
        public string MoreImages { set; get; }

        [Required]
        [MaxLength(500)]
        public string Description { set; get; }

        public string Content { set; get; }
        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }
        public string Tags { set; get; }

        [ForeignKey("SituationCategoryID")]
        public virtual SituationCategory SituationCategory { set; get; }

        [ForeignKey("SettleBodyID")]
        public virtual SettleBody SettleBody { set; get; }

        [ForeignKey("ResolvedSituationID")]
        public virtual ResolvedSituation ResolvedSituation { set; get; }
    }
}