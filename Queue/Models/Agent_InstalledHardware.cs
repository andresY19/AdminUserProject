using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Queue.Models
{
    [Table("Agent_InstalledHardware")]
    public class Agent_InstalledHardware
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Hardware { get; set; }
        public System.Guid Id_Empresa { get; set; }
        public string Pc { get; set; }
        public string UserName { get; set; }
        public Nullable<bool> Status { get; set; }

        public virtual Agent_Empresa Agent_Empresa { get; set; }
    }
}