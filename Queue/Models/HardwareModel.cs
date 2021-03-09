using Queue.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Queue.Models
{
    public class HardwareModel
    {
        public string token { get; set; }
        public List<InstalledHardwareViewModel> InstalledHardwareViewModel = new List<InstalledHardwareViewModel>();
    }
}