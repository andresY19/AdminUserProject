using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Queue.ViewModels
{
    public class InstalledProgramsViewModel
    {
        public BsonBinaryData _id { get; set; } = new BsonBinaryData(Guid.NewGuid(), GuidRepresentation.Standard);
        public string Name { get; set; }
        public string Vertion { get; set; }
        public DateTime? InstalledDate { get; set; }
        public string Size { get; set; }
        public string User { get; set; }
        public string Pc { get; set; }
        public string IdCompany { get; set; }
        public DateTime uninstalldate { get; set; } = DateTime.Now;
        public bool Status { get; set; } = true;
    }
}