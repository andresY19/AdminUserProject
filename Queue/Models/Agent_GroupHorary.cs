using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Queue.Models
{
    [Table("Agent_Horary")]
    public class Agent_GroupHorary
    {
        [Key]

        public Guid Id_GroupHorary { get; set; }
        [DisplayName("Nombre Grupo")]
        [Required]
        public string NameGroup { get; set; }

        
    }
}