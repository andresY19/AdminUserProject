using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Queue.Models
{
    [Table("Agent_Captures")]
    public class Agent_Captures
	{
        [Key]
        public Guid IdCapture { get; set; }
        public string UserName { get; set; }
        public string Pc { get; set; }
        public string Ip { get; set; }
        public byte[] Img { get; set; }
        public DateTime Date { get; set; }
        public Guid Id_Empresa { get; set; }

        public virtual Agent_Empresa Agent_Empresa { get; set; }
    }
}