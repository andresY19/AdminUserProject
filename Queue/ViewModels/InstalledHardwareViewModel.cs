using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Queue.ViewModels
{
    public class InstalledHardwareViewModel
    {
        public BsonBinaryData _id { get; set; } = new BsonBinaryData(Guid.NewGuid(), GuidRepresentation.Standard);        
        public string Type { get; set; }
        public string Hardware { get; set; }
        public string Pc { get; set; }
        public string User { get; set; }
        public bool status { get; set; } = true;
        public string IdCompany { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        public DateTime uninstalldate { get; set; } = DateTime.Now;
    }
}