using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static Queue.Utils.Enums;

namespace Queue.Models
{
    [Table("Agent_GroupHoraryDetail")]
    public class Agent_GroupHoraryDetail
    {
        [Key]
        public Guid Id_GroupHoraryDetail { get; set; }

        [DisplayName("Día")]
        [Required]
        public EnumDays Day { get; set; }

        [DisplayName("Hora Desde")]
        [DataType(DataType.Date, ErrorMessage = "El formato debe ser hh-mm-am/pm")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:tt}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime HourFrom { get; set; }

        [DisplayName("Hora Hasta")]
        [DataType(DataType.Date, ErrorMessage = "El formato debe ser hh-mm-am/pm")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:tt}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime HourUntil { get; set; }

        [DisplayName("Tipo")]
        [Required]
        public EnumType Type { get; set; }

        [ForeignKey("Agent_GroupHorary")]
        [DisplayName("Grupo Horario")]
        public Guid Id_GroupHorary { get; set; }
        public Agent_GroupHorary Agent_GroupHorary { get; set; }
    }
}