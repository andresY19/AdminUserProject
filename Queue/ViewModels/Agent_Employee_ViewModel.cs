using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Queue.ViewModels
{
    public class Agent_Employee_ViewModel
    {
        [DisplayName("Name")]
        public string Nombre { get; set; }

        [DisplayName("Address")]
        public string Direccion { get; set; }

        [DisplayName("Phone")]
        public string Telefono { get; set; }

        public string Email { get; set; }

        [DisplayName("Identification")]

        public string Identificacion { get; set; }

        [DisplayName("Username")]
        public string Usuario { get; set; }

        public string Cargo { get; set; }

        [NotMapped]
        [DisplayName("Posible Error")]
        public string posibleerror { get; set; }

    }
}