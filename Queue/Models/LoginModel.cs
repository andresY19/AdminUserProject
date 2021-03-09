using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Queue.Models
{
    public class LoginModel
    {
        public Guid Id_Usuario { get; set; }

        [Display(Name = "user")]
        [JsonProperty("user")]
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }

        [Display(Name = "token")]
        [JsonProperty("token")]
        public string token { get; set; }
    }
}