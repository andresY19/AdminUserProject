using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Queue.Models
{
    public class ConfigurationModel
    {
        public Guid Id_Configuration { get; set; }
        public Nullable<int> InactivityTime { get; set; }
        public Nullable<int> InactivityPeriod { get; set; }
        public Nullable<Guid> Id_Empresa { get; set; }

        [Display(Name = "token")]
        [JsonProperty("token")]
        public string token { get; set; }
    }
}