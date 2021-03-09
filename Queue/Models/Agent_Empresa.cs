using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Queue.Models
{
    [Table("Agent_Empresa")]
    public class Agent_Empresa
    {
        [Key]
        public Guid IdCompany { get; set; }
        [DisplayName("Name")]
        [Required]
        public string Nombre { get; set; }
        [DisplayName("Address")]
        [Required]
        public string Direccion { get; set; }
        [DisplayName("Phone")]
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Rut { get; set; }
        [Required]
        public string Key { get; set; }
        public decimal Id_EmpresaBPM { get; set; }
        [DisplayName("Status")]
        public bool status { get; set; } = true;
        public string string_status { get; set; }
    }
}