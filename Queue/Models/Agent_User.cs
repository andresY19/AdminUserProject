using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Queue.Models
{
    [Table("Agent_User")]
    public class Agent_User
    {
        [Key]
        public string Id_Usuario { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string token { get; set; }
        public bool IsLogged { get; set; }
        public Nullable<Guid> Id_Empresa { get; set; }

        public virtual Agent_Empresa Agent_Empresa { get; set; }
    }
}