using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Queue.Models
{
    public class schedule
    {
        public string UserName { get; set; }
        public DateTime? HourFrom { get; set; }
        public DateTime? HourUntil { get; set; }
    }
}