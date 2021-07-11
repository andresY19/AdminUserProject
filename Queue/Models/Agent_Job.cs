using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Queue.Models
{
    [Table("Agent_Job")]
    public class Agent_Job
    {
        [Key]
        public Guid idJob { get; set; }

        public Guid IdCompany { get; set; }

        [DisplayName("Cargo")]
        [Required]
        public string Cargo { get; set; }

        [DisplayName("Descripción")]
        [Required]
        public string Descripcion { get; set; }
                
        [DisplayName("Status")]
        public bool status { get; set; } = true;
        public string string_status { get; set; }

        public bool Lunes { get; set; }
        public bool Masrtes { get; set; }
        public bool Miercoles { get; set; }
        public bool Jueves { get; set; }
        public bool Viernes { get; set; }
        public bool Sabado { get; set; }
        public bool Domingo { get; set; }

        [DisplayName("Inicio jornada")]        
        public DateTime? HorarioIniciaLunes { get; set; }

        [DisplayName("Fin Jornada")]
        public DateTime? HorarioTerminaLunes { get; set; }

        [DisplayName("Inicio jornada")]
        public DateTime? HorarioIniciaMartes { get; set; }

        [DisplayName("Fin Jornada")]
        public DateTime? HorarioTerminaMartes { get; set; }

        [DisplayName("Inicio jornada")]
        public DateTime? HorarioIniciaMiercoles { get; set; }

        [DisplayName("Fin Jornada")]
        public DateTime? HorarioTerminaMiercoles { get; set; }

        [DisplayName("Inicio jornada")]
        public DateTime? HorarioIniciaJueves { get; set; }

        [DisplayName("Fin Jornada")]
        public DateTime? HorarioTerminaJueves { get; set; }

        [DisplayName("Inicio jornada")]
        public DateTime? HorarioIniciaViernes { get; set; }

        [DisplayName("Fin Jornada")]
        public DateTime? HorarioTerminaViernes { get; set; }

        [DisplayName("Inicio jornada")]
        public DateTime? HorarioIniciaSabado { get; set; }

        [DisplayName("Fin Jornada")]
        public DateTime? HorarioTerminaSabado { get; set; }

        [DisplayName("Inicio jornada")]
        public DateTime? HorarioIniciaDomingo { get; set; }

        [DisplayName("Fin Jornada")]
        public DateTime? HorarioTerminaDomingo { get; set; }

    }
}