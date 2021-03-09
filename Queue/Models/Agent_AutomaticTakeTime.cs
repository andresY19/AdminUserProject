using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Queue.Models
{
    [Table("Agent_AutomaticTakeTime")]
    public class Agent_AutomaticTakeTime
    {
        [Key]
        public Guid Id { get; set; }
        public string Application { get; set; }
        public string Title { get; set; }
        public Nullable<double> Time { get; set; }
        public Nullable<double> Inactivity { get; set; }
        public string Ip { get; set; }
        public string Pc { get; set; }
        public string UserName { get; set; }
        public Nullable<Guid> Id_Empresa { get; set; }
        public System.DateTime Date { get; set; }

        public virtual Agent_Empresa Agent_Empresa { get; set; }
    }
}