using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Queue.ViewModels
{
    public class PrinterPost
    {
        public string status { get; set; }
        public string printerMAC { get; set; }
        public string uniqueID { get; set; }
        public string statusCode { get; set; }
        public string clientAction { get; set; }
    }
}