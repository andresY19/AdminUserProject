using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Queue.Models
{
    [Table("Agent_ProgramClasification")]
    public class Agent_ProgramClasification
    {
        [Key]
        public Guid idprogramclasification { get; set; }

        [DisplayName("Nombre")]
        [Required]
        public string name { get; set; }

        [DisplayName("Titulo")]
        public string title { get; set; }

        [DisplayName("Clasificación")]
        [Required]
        public int clasification { get; set; }

        public virtual Agent_Empresa Agent_Empresa { get; set; }

        [NotMapped]
        public List<AutomaticTakeTimeModel> AutomaticTakeTime = new List<AutomaticTakeTimeModel>();
    }
}