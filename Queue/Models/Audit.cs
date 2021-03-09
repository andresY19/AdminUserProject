using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Queue.Models
{
    [Table("Audit")]
    public class Audit
    {
        [Key]
        [DisplayName("Audit Id")]
        public int idaudit { get; set; }
        public Guid user { get; set; }
        public string table { get; set; }
        public string field { get; set; }
        public string oldvalue { get; set; }
        public string newvalue { get; set; }
        public DateTime datechange { get; set; }
        public string Action { get; set; }
    }
}