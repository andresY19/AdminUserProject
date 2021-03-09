using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Queue.Models
{
    [Table("Agent_Configuration")]
    public class Agent_Configuration
    {
        [Key]
        
        public Guid Id_Configuration { get; set; }
        [Required]
        [DisplayName("Inactivity Period (in seconds)")]
        public int InactivityPeriod { get; set; } = 10;
        [Required]
        [DisplayName("Upload Frecuency (in seconds)")]
        public int UploadFrecuency { get; set; } = 10;
        [Required]
        [DisplayName("Capture Images Frecuency (in seconds)")]
        public int CaptureFrecuency { get; set; } = 10;
        public Guid? IdCompany { get; set; }
        public virtual Agent_Empresa Agent_Empresa { get; set; }
    }
}