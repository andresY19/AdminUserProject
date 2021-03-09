using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Queue.Models
{
    [Table("AuditAction")]
    public class AuditAction
    {
        [Key]
        [DisplayName("AuditAction Id")]
        public int idauditaction { get; set; }

        public Guid user { get; set; }
        public string module { get; set; }
        public string action { get; set; }
        public DateTime date { get; set; }
    }
}