using Queue.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Queue.Models
{
    public class InstalledModel
    {
        public string token { get; set; }
        public List<InstalledProgramsViewModel> InstalledProgramsViewModel = new List<InstalledProgramsViewModel>();
    }
}