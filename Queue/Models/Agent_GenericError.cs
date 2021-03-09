using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Queue.Models
{
    [Table("Agent_GenericError")]
    public class Agent_GenericError
    {
        [Key]
        public int codigo_id { get; set; }
        public string Codigo { get; set; }
        public string Message { get; set; }
    }
}