using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Queue.Models
{
    [Table("Agent_CompanyDepartment")]
    public class Agent_CompanyDepartment
    {
        public Guid Id { get; set; }
        public Guid IdCompany { get; set; }
        public string Name { get; set; }
        public Agent_Empresa Agent_Empresa { get; set; }

    }
}