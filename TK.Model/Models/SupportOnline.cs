using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TK.Model.Models
{
    [Table("SupportOnlines")]
    public class SupportOnline
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(50)]
        public String Name { set; get; }

        [MaxLength(50)]
        public String Department { set; get; }

        [MaxLength(50)]
        public String Skype { set; get; }

        [MaxLength(50)]
        public String Mobile { set; get; }

        [MaxLength(50)]
        public String Email { set; get; }

        [MaxLength(50)]
        public String Yahoo { set; get; }

        [MaxLength(50)]
        public String Facebook { set; get; }

        public bool Status { set; get; }
        public int? DisplayOrder { set; get; }
    }
}