using System;
using System.ComponentModel.DataAnnotations;

namespace Queue.Models
{
    public class Agent_UserCompanies
    {
        [Key]
        public Guid idUser { get; set; }
        public Guid IdCompany { get; set; }
    }
}