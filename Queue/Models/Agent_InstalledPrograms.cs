using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Queue.Models
{
    [Table("Agent_InstalledPrograms")]
    public class Agent_InstalledPrograms
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Vertion { get; set; }
        public Nullable<System.DateTime> InstallDate { get; set; }
        public Nullable<int> Size { get; set; }
        public System.Guid Id_Empresa { get; set; }
        public string Pc { get; set; }
        public string UserName { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }

        public virtual Agent_Empresa Agent_Empresa { get; set; }
    }
}